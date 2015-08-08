using System;

namespace Glimpse
{
    public class Settings : ISettings
    {
        public Func<bool> ShouldProfile { get; set; }
    }
}