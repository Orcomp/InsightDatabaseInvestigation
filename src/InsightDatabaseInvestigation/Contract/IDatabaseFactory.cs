namespace InsightDatabaseInvestigation.Contract
{
    using System.Data;

    public interface IDatabaseFactory
    {
        IDbConnection GetOpenConnection();
    }
}