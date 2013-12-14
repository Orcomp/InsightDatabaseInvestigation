namespace InsightDatabaseInvestigation.Initializers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Data.SQLite;
    using System.Linq;

    using Insight.Database;

    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.Model;

    public class SQliteDatabaseInitializer : IDatabaseInitializer
    {
        private const string CreateTablesCommandSql = @".\Scripts\SQlite\createCommand.sql";
        private readonly IDatabaseFactory _databaseFactory;
        
        public SQliteDatabaseInitializer(IDatabaseFactory databaseFactory, bool dropOnCreate = false)
        {
            DropOnCreate = dropOnCreate;
            _databaseFactory = databaseFactory;
        }

        public bool DropOnCreate { get; set; }
        
        public void CreateOrUpdate()
        {
            var databaseFileExists = File.Exists(@".\data.db");

            if (DropOnCreate && databaseFileExists)
            {
                File.Delete(@".\data.db");
            }

            if (DropOnCreate || !databaseFileExists)
            {
                // create fresh db
                SQLiteConnection.CreateFile(@".\data.db");
            }
            
            // setup database tables
            var connection = _databaseFactory.GetOpenConnection();
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandText = File.ReadAllText(CreateTablesCommandSql);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            dbCommand.ExecuteNonQuery();
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
                foreach (var user in users)
                {
                    openConnection.InsertSql("INSERT INTO Users (FirstName, LastName) values (@FirstName, @LastName); SELECT ROWID FROM Users order by ROWID DESC limit 1",user);
                }

                foreach (var userGroup in userGroups)
                {
                    openConnection.InsertSql("INSERT INTO UserGroup (Name) values (@Name); SELECT ROWID FROM UserGroup order by ROWID DESC limit 1", userGroup);
                }

                // NOTE: After inserting the objects into the database, they will automagically be assigned the correct ID
                var memberships = new List<Membership>();

                foreach (var userGroup in userGroups)
                {
                    foreach (var user in userGroup.Users)
                    {
                        memberships.Add(new Membership() { UserID = user.ID, UserGroupID = userGroup.ID });
                    }
                }

                foreach (var membership in memberships)
                {
                    openConnection.InsertSql("INSERT INTO Membership (UserGroupID , UserID) values (@UserGroupID, @UserID); SELECT ROWID FROM Membership order by ROWID DESC limit 1", membership);
                }
            }
        }
    }
}