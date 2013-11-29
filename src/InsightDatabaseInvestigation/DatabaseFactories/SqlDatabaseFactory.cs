namespace InsightDatabaseInvestigation.DatabaseFactories
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using InsightDatabaseInvestigation.Contract;

    public class SqlDatabaseFactory : IDatabaseFactory
    {
        public IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SQLExpress"].ToString();
        }
    }
}