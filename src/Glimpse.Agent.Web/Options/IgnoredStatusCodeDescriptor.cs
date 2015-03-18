using System;

namespace Glimpse.Agent.Web.Options
{
    public class IgnoredStatusCodeDescriptor
    {
        public IgnoredStatusCodeDescriptor(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; } 
    }
}