using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Glimpse.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string Serialize(this JsonSerializer jsonSerializer, object data)
        {
            // Brought across from - https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/JsonConvert.cs#L635
            var stringBuilder = new StringBuilder(256);
            using (var stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonSerializer.Serialize(jsonWriter, data, data.GetType());

                return stringWriter.ToString();
            }
        }
    }
}