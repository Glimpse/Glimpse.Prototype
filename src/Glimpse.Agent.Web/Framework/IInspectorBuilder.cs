using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.Web
{
    public interface IInspectorBuilder
    {
        IApplicationBuilder AppBuilder { get; }

        //TODO: Add all Use overloads
        IInspectorBuilder Use(Func<HttpContext, Func<Task>, Task> middleware);
    }
}