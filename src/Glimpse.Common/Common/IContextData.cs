using System;

namespace Glimpse
{
    public interface IContextData<T>
    {
        T Value { get; set; }
    }
}