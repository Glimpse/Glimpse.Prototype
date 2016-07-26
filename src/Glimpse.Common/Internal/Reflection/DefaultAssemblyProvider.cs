using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Glimpse.Internal
{
    public class DefaultAssemblyProvider : IAssemblyProvider
    {
        private static HashSet<string> ReferenceAssemblies { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Glimpse.Common",
            "Glimpse.Server",
            "Glimpse.Agent.AspNet",
            "Glimpse.Agent.AspNet.Mvc"
        };

        private readonly DependencyContext _dependencyContext;
        private readonly Assembly _entryAssembly;

        public DefaultAssemblyProvider(IHostingEnvironment env)
        {
            var entryPointAssemblyName = env.ApplicationName;
            _entryAssembly = Assembly.Load(new AssemblyName(entryPointAssemblyName));
            _dependencyContext = DependencyContext.Load(Assembly.Load(new AssemblyName(entryPointAssemblyName)));
        }

        public IEnumerable<Assembly> GetCandidateAssemblies(string coreLibrary)
        {
            // TODO: currently we aren't using the provided coreLibrary,
            //       we should switch over to this later on once the
            //       implementation has been proved out.

            if (_dependencyContext == null)
            {
                // Use the entry assembly as the sole candidate.
                return new[] { _entryAssembly };
            }

            return GetCandidateLibraries(_dependencyContext)
                .SelectMany(library => library.GetDefaultAssemblyNames(_dependencyContext))
                .Select(Assembly.Load);
        }

        // Returns a list of libraries that references the assemblies in <see cref="ReferenceAssemblies"/>.
        // By default it returns all assemblies that reference any of the primary MVC assemblies
        // while ignoring MVC assemblies.
        // Internal for unit testing
        private IEnumerable<RuntimeLibrary> GetCandidateLibraries(DependencyContext dependencyContext)
        {
            if (ReferenceAssemblies == null)
            {
                return Enumerable.Empty<RuntimeLibrary>();
            }

            var candidatesResolver = new CandidateResolver(dependencyContext.RuntimeLibraries, ReferenceAssemblies);
            return candidatesResolver.GetCandidates();
        }

        private class CandidateResolver
        {
            private readonly IDictionary<string, Dependency> _dependencies;

            public CandidateResolver(IReadOnlyList<RuntimeLibrary> dependencies, ISet<string> referenceAssemblies)
            {
                _dependencies = dependencies
                    .ToDictionary(d => d.Name, d => CreateDependency(d, referenceAssemblies), StringComparer.OrdinalIgnoreCase);
            }

            private Dependency CreateDependency(RuntimeLibrary library, ISet<string> referenceAssemblies)
            {
                var classification = DependencyClassification.Unknown;
                if (referenceAssemblies.Contains(library.Name))
                {
                    classification = DependencyClassification.GlimpseReference;
                }

                return new Dependency(library, classification);
            }

            private DependencyClassification ComputeClassification(string dependency)
            {
                var candidateEntry = _dependencies[dependency];
                if (candidateEntry.Classification != DependencyClassification.Unknown)
                {
                    return candidateEntry.Classification;
                }
                else
                {
                    var classification = DependencyClassification.NotCandidate;
                    foreach (var candidateDependency in candidateEntry.Library.Dependencies)
                    {
                        var dependencyClassification = ComputeClassification(candidateDependency.Name);
                        if (dependencyClassification == DependencyClassification.Candidate ||
                            dependencyClassification == DependencyClassification.GlimpseReference)
                        {
                            classification = DependencyClassification.Candidate;
                            break;
                        }
                    }

                    candidateEntry.Classification = classification;

                    return classification;
                }
            }

            public IEnumerable<RuntimeLibrary> GetCandidates()
            {
                foreach (var dependency in _dependencies)
                {
                    var classification = ComputeClassification(dependency.Key);
                    if (classification == DependencyClassification.Candidate
                        || classification == DependencyClassification.GlimpseReference)
                    {
                        yield return dependency.Value.Library;
                    }
                }
            }

            private class Dependency
            {
                public Dependency(RuntimeLibrary library, DependencyClassification classification)
                {
                    Library = library;
                    Classification = classification;
                }

                public RuntimeLibrary Library { get; }

                public DependencyClassification Classification { get; set; }

                public override string ToString()
                {
                    return $"Library: {Library.Name}, Classification: {Classification}";
                }
            }

            private enum DependencyClassification
            {
                Unknown = 0,
                Candidate = 1,
                NotCandidate = 2,
                GlimpseReference = 3
            }
        }
    }
}