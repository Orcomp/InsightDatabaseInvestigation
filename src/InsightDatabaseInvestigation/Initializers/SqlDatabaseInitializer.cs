namespace InsightDatabaseInvestigation.Initializers
{
    using System;
    using System.Data;
    using System.IO;
    using InsightDatabaseInvestigation.Contract;

    public class SqlDatabaseInitializer : IDatabaseInitializer
    {
        private const string CreateStoredProcedures = @".\Scripts\MSSQL\createStoredProcs.sql";
        private const string CreateTablesCommandSql = @".\Scripts\MSSQL\createCommand.sql";

        private readonly IDatabaseFactory _databaseFactory;

        public SqlDatabaseInitializer(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public void Create()
        {
            var connection = _databaseFactory.GetOpenConnection();
            var dbCommand = connection.CreateCommand();

            // create database tables
            dbCommand.CommandText = File.ReadAllText(CreateTablesCommandSql);
            dbCommand.ExecuteNonQuery();

            // create stored procedures
            dbCommand.CommandText = File.ReadAllText(CreateStoredProcedures);
            dbCommand.ExecuteNonQuery();
        }

        public void Seed()
        {
        }
    }
}