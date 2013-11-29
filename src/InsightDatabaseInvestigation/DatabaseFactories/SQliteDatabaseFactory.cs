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
            return new SQLiteConnection(GetConnectionString());
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQlite"].ToString();
        }
    }
}