using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Glimpse.Server.Web.Framework
{
    public interface IAuthorizeAgent
    {
        bool AllowAgent(HttpContext context);
    }
}
