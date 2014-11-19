using System;
using System.Collections.Generic;

namespace Glimpse
{
    public interface IDiscoverableCollection<T> : ICollection<T>
    {    
        void Discover();
    }
}