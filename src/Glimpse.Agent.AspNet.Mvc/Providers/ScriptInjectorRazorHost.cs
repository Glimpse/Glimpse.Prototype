using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Razor.Directives;
using Microsoft.AspNet.Razor.Chunks;

namespace Glimpse.Agent.AspNet.Mvc
{
    public class ScriptInjectorRazorHost : MvcRazorHost
    {
        public ScriptInjectorRazorHost(IChunkTreeCache codeTreeCache) : base(codeTreeCache)
        {
        }

        private IReadOnlyList<Chunk> _overriddenChunks;

        public override IReadOnlyList<Chunk> DefaultInheritedChunks
        {
            get
            {
                if (_overriddenChunks != null)
                    return _overriddenChunks;

                var original = base.DefaultInheritedChunks.ToArray();
                var newArray = new Chunk[original.Length + 1];

                original.CopyTo(newArray, 0);

                newArray[original.Length] = new AddTagHelperChunk
                {
                    LookupText = "*, Glimpse.Agent.AspNet.Mvc"
                };

                _overriddenChunks = newArray;

                return _overriddenChunks;
            }
        }
    }
}
