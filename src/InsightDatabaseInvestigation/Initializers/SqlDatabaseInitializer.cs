namespace InsightDatabaseInvestigation.Initializers
{
    using System;
    using System.Data.Common;
    using System.Linq;
    using Insight.Database.Schema;
    using Insight.Database;
    using Model;
    using Contract;

    public class SqlDatabaseInitializer : IDatabaseInitializer
    {
        private const string CreateStoredProcedures = @".\Scripts\MSSQL\createStoredProcs.sql";
        private const string CreateTablesCommandSql = @".\Scripts\MSSQL\createCommand.sql";

        private readonly IDatabaseFactory _databaseFactory;

        public SqlDatabaseInitializer(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public void CreateOrUpdate()
        {
            if (!SchemaInstaller.DatabaseExists(_databaseFactory.GetConnectionString()))
            {
                SchemaInstaller.CreateDatabase(_databaseFactory.GetConnectionString());
            }

            var tablesSchema = new SchemaObjectCollection(CreateTablesCommandSql);
            var storedProcSchema = new SchemaObjectCollection(CreateStoredProcedures);
            
            var schemaInstaller = new SchemaInstaller(_databaseFactory.GetOpenConnection() as DbConnection);

            schemaInstaller.Install("tables", tablesSchema);
            schemaInstaller.Install("storedProcs", storedProcSchema);
        }

        public void Seed()
        {
            var random = new Random();

            var users = Enumerable.Range(0, 100).Select(ix => new User()
                {
                    Comment = "Some comment and random text " + GetRandomString(), Email = "test@me.com", Middle = "V", FirstName = GetFirstName(), LastName = GetLastName(), Phone = GetPhoneNumber()
                }).ToList();

            var userGroups = Enumerable.Range(0, 5).Select(ix => new UserGroup()
                {
                    Comment = "Comment on group",
                    Name = "Group " + ix
                }).ToList();

            using (var openConnection = _databaseFactory.GetOpenConnection())
            {
                openConnection.InsertList("InsertUsers", users);
                openConnection.InsertList("InsertUserGroups", userGroups);
                
                var minUserId = users.Min(u => u.UserID);
                var maxUserId = users.Max(u => u.UserID);
                var minUserGroupId = userGroups.Min(u => u.GroupID);
                var maxUserGroupId = userGroups.Max(u => u.GroupID);

                var memberships = Enumerable.Range(0, 1000).Select(ix => new Membership()
                {
                    UserID = random.Next(minUserId, maxUserId),
                    GroupID = random.Next(minUserGroupId, maxUserGroupId)
                }).Distinct(new UniqueMembershipEqualityProvider()).ToList();

                openConnection.InsertList("InsertMemberships", memberships);
            }
        }

        private string GetPhoneNumber()
        {
            return "0458797450";
        }

        private string GetLastName()
        {
            return "Last Name";
        }

        private string GetFirstName()
        {
            return "Pieter";
        }

        private string GetRandomString()
        {
            return "Random comment";
        }
    }
}