using System;
using System.Collections;
using System.Collections.Generic;

namespace Glimpse.Reflection
{
    public class ReflectionDiscoverableCollection<T> : List<T>, IDiscoverableCollection<T>
    {
        private readonly ITypeService _typeService;

        public ReflectionDiscoverableCollection(ITypeService typeService)
        {
            _typeService = typeService;
            CoreLibarary = "Glimpse";
        }

        public string CoreLibarary { get; set; }

        public void Discover()
        {
            var instances = _typeService.Resolve<T>(CoreLibarary);

            AddRange(instances);
        }
    }
}