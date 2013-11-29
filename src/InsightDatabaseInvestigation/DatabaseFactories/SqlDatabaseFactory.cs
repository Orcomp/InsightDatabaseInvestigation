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
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SQLExpress"].ToString());
        }
    }
}