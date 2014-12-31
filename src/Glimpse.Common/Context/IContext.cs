using System;

namespace Glimpse
{
    public interface IContext
    {
        IServiceProvider GlobalServices { get; }

        IServiceProvider LocalServices { get; }
    }
}