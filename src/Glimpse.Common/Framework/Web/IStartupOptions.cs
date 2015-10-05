using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Web
{
    public interface IStartupOptions
    {
        IServiceProvider ApplicationServices { get; }
    }
}
