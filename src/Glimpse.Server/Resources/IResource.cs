using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Resources
{
    public interface IResource
    {
        Task Invoke(HttpContext context, IDictionary<string, string> parameters);

        string Name { get; }

        ResourceParameters Parameters { get; }

        ResourceType Type { get; }
    }
}