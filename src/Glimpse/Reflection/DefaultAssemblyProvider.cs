using Microsoft.Framework.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glimpse.Reflection
{
    public class DefaultAssemblyProvider : IAssemblyProvider
    {
        private readonly ILibraryManager _manager;

        public DefaultAssemblyProvider(ILibraryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary)
        {
            var libraries = _manager.GetReferencingLibraries(coreLibrary);
            var assemblyNames = libraries.SelectMany(l => l.LoadableAssemblies);
            var assemblies = assemblyNames.Select(x => Assembly.Load(x));

            return assemblies;
        }
    }
}