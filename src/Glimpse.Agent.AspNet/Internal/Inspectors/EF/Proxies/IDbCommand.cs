namespace Glimpse.Agent.Internal.Inspectors.EF.Proxies
{
    public interface IDbCommand
    {
        string CommandText { get; }

        int CommandType { get; }

        object Parameters { get; }
    }
}
