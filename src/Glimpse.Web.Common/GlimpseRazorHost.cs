using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Razor.Directives;
using Microsoft.AspNet.Razor.Generator.Compiler;

namespace Glimpse.Host.Web.AspNet
{
    public class GlimpseRazorHost : MvcRazorHost
    {
        public GlimpseRazorHost(ICodeTreeCache codeTreeCache) : base(codeTreeCache)
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
                    LookupText = "*, Glimpse.Host.Web.AspNet"
                };

                _overriddenChunks = newArray;

                return _overriddenChunks;
            }
        }
    }
}
