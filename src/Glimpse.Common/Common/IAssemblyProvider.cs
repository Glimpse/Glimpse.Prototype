using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glimpse
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary);
    }
}