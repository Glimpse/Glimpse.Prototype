using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse.Reflection
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary);
    }
}