using System;

namespace Glimpse.Agent.Messages
{
    public class AfterExecuteCommandMessage
    {
        public string CommandText { get; set; }

        public int CommandType { get; set; }

        public object CommandParameters { get; set; }

        public string CommandMethod { get; set; }

        public bool CommandIsAsync { get; set; }

        public TimeSpan CommandDuration { get; set; }

        public DateTime CommandStartTime { get; set; }

        public DateTime CommandEndTime { get; set; }

        public bool CommandHadException { get; set; }

        // TODO: Need to decide what I want to pull out here
        //public Exception CommandException { get; set; }
    }
}
