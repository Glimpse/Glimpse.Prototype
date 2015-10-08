namespace Glimpse.Agent.Internal.Inspectors.EF.Proxies
{
    public interface IDbCommand
    {
        string CommandText { get; set; }

        int CommandType { get; set; }

        object Parameters { get; set; }
    }
}
