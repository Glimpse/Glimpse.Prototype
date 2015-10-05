using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Glimpse.Agent.AspNet
{
    public interface IInspectorFunctionBuilder
    {
        IApplicationBuilder AppBuilder { get; }

        IInspectorFunctionBuilder Use(Func<HttpContext, Func<Task>, Task> middleware);
    }
}