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
            var sqLiteConnection = new SQLiteConnection(GetConnectionString());
            sqLiteConnection.Open();
            return sqLiteConnection;
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQlite"].ToString();
        }
    }
}