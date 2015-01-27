using System;

namespace Glimpse
{
    public class Settings : ISettings
    {
        public Func<IContext, bool> ShouldProfile { get; set; }
    }
}