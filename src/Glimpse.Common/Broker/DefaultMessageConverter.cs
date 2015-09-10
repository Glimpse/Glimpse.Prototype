using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Glimpse.Common;

namespace Glimpse
{
    public class DefaultMessageConverter : IMessageConverter
    {
        private readonly JsonSerializer _jsonSerializer;

        private readonly static Type _objectType = typeof(object);
        private readonly static Type _dictionaryType = typeof(Dictionary<string, object>);
        private readonly static MethodInfo _addMethodInfo = _dictionaryType.GetMethod("Add", new[] { typeof(string), typeof(object) });
        private readonly static ConstructorInfo _constructorInfo = typeof(ReadOnlyDictionary<string, object>).GetConstructor(new [] { _dictionaryType });
        private readonly static ConcurrentDictionary<Type, Func<object, IReadOnlyDictionary<string, object>>> _methodCache = new ConcurrentDictionary<Type, Func<object, IReadOnlyDictionary<string, object>>>();
        private static int _ordinal = 0;

        private readonly static Type[] _exclusions = { typeof(object) };


        public DefaultMessageConverter(JsonSerializer jsonSerializer)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            _jsonSerializer = jsonSerializer;
        }

        public IMessage ConvertMessage(object payload, MessageContext context)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                Ordinal = Interlocked.Increment(ref _ordinal),
                Types = GetTypes(payload),
                Payload = _jsonSerializer.Serialize(payload),
                Context = context,
                Indices = GetIndices(payload)
            };

            message.Payload = _jsonSerializer.Serialize(message);

            return message;
        }

        private static IEnumerable<string> GetTypes(object payload)
        {
            var typeInfo = payload.GetType().GetTypeInfo();

            return typeInfo.BaseTypes(includeSelf: true)
                .Concat(typeInfo.ImplementedInterfaces)
                .Except(_exclusions)
                .Select(t => t.KebabCase());
        }

        private static IReadOnlyDictionary<string, object> GetIndices(object payload)
        {
            var payloadType = payload.GetType();

            var indicesCreator = _methodCache.GetOrAdd(payloadType, GenerateIndicesCreator);

            return indicesCreator(payload);
        }

        private static Func<object, IReadOnlyDictionary<string, object>> GenerateIndicesCreator(Type messageType)
        {
            /* Creates a func that looks something like this:
            ** Func<object, IReadOnlyDictionary<string, object>> lambda = message =>
            ** {
            **     var casted = (<Message Type>) message;
            **     return new ReadOnlyDictionary<string, object>(new Dictionary<string, object>
            **     {
            **         { "<PromoteToAttribute.Key>", message.<Property Name> },
            **         ... // Repeat as many times as necessary
            **     });
            ** };
            */

            var parameter = Expression.Parameter(_objectType, "message");
            var variable = Expression.Variable(messageType, "casted");
            var cast = Expression.Assign(variable, Expression.Convert(parameter, messageType));

            var items = new List<ElementInit>();

            foreach (var property in messageType.GetProperties())
            {
                var attribute =
                    property.GetCustomAttributes(typeof (PromoteToAttribute), true)
                        .Cast<PromoteToAttribute>()
                        .SingleOrDefault();
                if (attribute != null)
                    items.Add(Expression.ElementInit(_addMethodInfo, Expression.Constant(attribute.Key),
                        Expression.Convert(Expression.Property(variable, property.Name), _objectType)));
            }

            var ctor = Expression.New(_dictionaryType);

            var initItems = items.Count > 0;
            ListInitExpression init = null;
            if (initItems)
                init = Expression.ListInit(ctor, items);

            var wrapped = Expression.New(_constructorInfo, initItems ? (Expression)init : ctor);

            var code = Expression.Block(new[] { variable }, cast, initItems ? (Expression)init : ctor, wrapped);

            var lambda = Expression.Lambda<Func<object, IReadOnlyDictionary<string, object>>>(code, parameter);
            return lambda.Compile();
        }
    }
}