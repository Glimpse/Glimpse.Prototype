namespace Glimpse.Agent.Messages
{
    public class BeforeExecuteCommandMessage
    {
        public string CommandMethod { get; set; }

        public bool CommandIsAsync { get; set; }
    }
}