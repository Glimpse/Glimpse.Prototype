using System;
using System.Collections.Generic;

namespace Glimpse.Server
{
    public class GlimpseServerWebOptions
    {
        public GlimpseServerWebOptions()
        {
            AllowedUserRoles = new List<string>();
        }

        public bool AllowRemote { get; set; }

        public IList<string> AllowedUserRoles { get; }
    }
}