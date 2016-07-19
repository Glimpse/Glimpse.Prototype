using System.Threading.Tasks;
using Glimpse.Internal.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Glimpse.Server.Resources
{
    public class Json : IResponse
    {
        private readonly object _obj;
        private readonly string _contentType;
        public Json(object obj) : this(obj, "application/json")
        {
        }

        public Json(object obj, string contentType)
        {
            _obj = obj;
            _contentType = contentType;
        }

        public async Task Respond(HttpContext context)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Configure();

            var json = jsonSerializer.Serialize(_obj);

            var response = new RawJson(json, _contentType);
            await response.Respond(context);
        }
    }
}