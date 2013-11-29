namespace InsightDatabaseInvestigation.DatabaseFactories
{
    using System.Configuration;
    using System.Data;
    using System.Data.SQLite;
    using InsightDatabaseInvestigation.Contract;

    public class SQliteDatabaseFactory : IDatabaseFactory
    {
        public IDbConnection GetOpenConnection()
        {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQlite"].ToString());
        }
    }
}