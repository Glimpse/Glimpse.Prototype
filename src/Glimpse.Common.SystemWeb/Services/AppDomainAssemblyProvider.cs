using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glimpse.Services
{
    public class AppDomainAssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(x => !AppDomainAssemblyBlackList.IsBlackListed(x));
        }
    }
}