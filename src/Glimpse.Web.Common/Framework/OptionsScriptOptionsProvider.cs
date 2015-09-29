using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;

namespace Glimpse.Web
{
    public class OptionsScriptOptionsProvider : IScriptOptionsProvider
    {
        private readonly ScriptOptions _scriptOptions;

        public OptionsScriptOptionsProvider(IOptions<ScriptOptions> optionsAccessor)
        {
            _scriptOptions = optionsAccessor.Value;
        }

        public ScriptOptions BuildInstance()
        {
            return _scriptOptions;
        }
    }
}
