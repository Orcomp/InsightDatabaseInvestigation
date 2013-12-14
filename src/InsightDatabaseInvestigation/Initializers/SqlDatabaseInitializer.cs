namespace InsightDatabaseInvestigation.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.IO;
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
            // We always want to start with a new copy
            SchemaInstaller.DropDatabase(_databaseFactory.GetConnectionString());
            SchemaInstaller.CreateDatabase(_databaseFactory.GetConnectionString());

            var tablesSchema = new SchemaObjectCollection(CreateTablesCommandSql);
            var storedProcSchema = new SchemaObjectCollection(CreateStoredProcedures);
            
            var schemaInstaller = new SchemaInstaller(_databaseFactory.GetOpenConnection() as DbConnection);

            schemaInstaller.Install("tables", tablesSchema);
            schemaInstaller.Install("storedProcs", storedProcSchema);
        }

        public void Seed()
        {
            var users = new List<User>
                {
                    new User() { FirstName = "David", LastName = "Smith" },
                    new User() { FirstName = "John", LastName = "Jones" },
                    new User() { FirstName = "Marie", LastName = "Coles" },
                    new User() { FirstName = "Anne", LastName = "Brown" },
                    new User() { FirstName = "Bernard", LastName = "Green" },
                };

            var userGroups = new List<UserGroup>()
                {
                    new UserGroup() { Name = "Group1" }, // As no users
                    new UserGroup() { Name = "Group2", Users = users }, // Has all the users
                    new UserGroup() { Name = "Group3", Users = new List<User>() { users[0], users[1], users[4] } }, // All the Men
                    new UserGroup() { Name = "Group4", Users = new List<User>() { users[2], users[3] } }, // All the women
                };

            using (var openConnection = _databaseFactory.GetOpenConnection())
            {
                openConnection.InsertList("InsertUsers", users);
                openConnection.InsertList("InsertUserGroups", userGroups);

                // NOTE: After inserting the objects into the database, they will automagically be assigned the correct ID
                var memberships = new List<Membership>();

                foreach (var userGroup in userGroups)
                {
                    foreach (var user in userGroup.Users)
                    {
                        memberships.Add(new Membership() { UserID = user.ID, UserGroupID = userGroup.ID });
                    }
                }

                openConnection.InsertList("InsertMemberships", memberships);
            }
        }
    }
}