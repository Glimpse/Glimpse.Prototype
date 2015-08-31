using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Glimpse
{
    public class DefaultMessageConverter : IMessageConverter
    {
        private readonly JsonSerializer _jsonSerializer;

        private readonly static Type _objectType = typeof(object);
        private readonly static Type _dictionaryType = typeof(Dictionary<string, object>);
        private readonly static MethodInfo _addMethodInfo = _dictionaryType.GetMethod("Add", new[] { typeof(string), typeof(object) });
        private readonly static ConstructorInfo _constructorInfo = typeof(ReadOnlyDictionary<string, object>).GetConstructor(new [] { _dictionaryType });
        private readonly static IDictionary<Type, Func<object, IReadOnlyDictionary<string, object>>> _methodCache = new Dictionary<Type, Func<object, IReadOnlyDictionary<string, object>>>();


        public DefaultMessageConverter(JsonSerializer jsonSerializer)
        {
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            _jsonSerializer = jsonSerializer;
        }

        public IMessage ConvertMessage(object payload, MessageContext context)
        { 
            var message = new Message();
            message.Id = Guid.NewGuid();
            message.Types = new [] { payload.GetType().FullName }; // TODO: Get all types, not just one
            message.Payload = _jsonSerializer.Serialize(payload);
            message.Context = context;

            ProcessIndices(payload, message);

            return message;
        } 

        private void ProcessIndices(object payload, Message message)
        {
            var payloadType = payload.GetType();
            Func<object, IReadOnlyDictionary<string, object>> indicesCreator;

            if (_methodCache.ContainsKey(payloadType))
            {
                indicesCreator = _methodCache[payloadType];
            }
            else
            {
                indicesCreator = GenerateIndicesCreator(payloadType);
                _methodCache.Add(payloadType, indicesCreator);
            }

            message.Indices = indicesCreator(payload);
        }

        private static Func<object, IReadOnlyDictionary<string, object>> GenerateIndicesCreator(Type messageType)
        {
            var parameter = Expression.Parameter(_objectType, "message");
            var variable = Expression.Variable(messageType, "casted");
            var cast = Expression.Assign(variable, Expression.Convert(parameter, messageType));

            var items = new List<ElementInit>();

            foreach (var property in messageType.GetProperties())
            {
                var attribute =
                    property.GetCustomAttributes(typeof(PromoteToAttribute), true)
                        .Cast<PromoteToAttribute>()
                        .SingleOrDefault();
                if (attribute != null)
                    items.Add(Expression.ElementInit(_addMethodInfo, Expression.Constant(attribute.Key),
                        Expression.Convert(Expression.Property(variable, property.Name), _objectType)));
            }

            var ctor = Expression.New(_dictionaryType);
            var init = Expression.ListInit(ctor, items);
            var wrapped = Expression.New(_constructorInfo, init);

            var code = Expression.Block(new[] { variable }, cast, init, wrapped);

            var lambda = Expression.Lambda<Func<object, IReadOnlyDictionary<string, object>>>(code, parameter);
            return lambda.Compile();
        }
    }
}