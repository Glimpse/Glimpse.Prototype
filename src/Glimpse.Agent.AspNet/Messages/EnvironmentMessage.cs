using System.Collections;

namespace Glimpse.Agent.AspNet.Messages
{
    //TODO: This is just an example to get an environment message up and running. More thought should be put into this
    public class EnvironmentMessage
    {
        public string Server { get; set; }
        public string OperatingSystem { get; set; }
        public int ProcessorCount { get; set; }
        public bool Is64Bit { get; set; }
        public string[] CommandLineArgs { get; set; }
        public IDictionary EnvironmentVariables { get; set; }
    }
}
