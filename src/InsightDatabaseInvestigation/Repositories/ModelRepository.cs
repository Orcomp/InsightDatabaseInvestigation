namespace InsightDatabaseInvestigation.Repositories
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Insight.Database;
    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.Model;

    public class ModelRepository : IModelRepository
    {
        public ModelRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        public IDatabaseFactory DatabaseFactory { get; set; }

        public Model GetModel()
        {
            string commandText = @"GetModel";
            var result = DatabaseFactory.GetOpenConnection().QueryResults<User, UserGroup, Membership>(commandText);

            var model = new Model();

            var users = result.Set1.ToDictionary(x => x.ID, x => x);
            var userGroups = result.Set2.ToDictionary(x => x.ID, x => x);

            foreach (var membership in result.Set3)
            {
                if (users.ContainsKey(membership.UserID) && userGroups.ContainsKey(membership.UserGroupID))
                {
                    membership.User = users[membership.UserID];
                    membership.UserGroup = userGroups[membership.UserGroupID];

                    membership.UserGroup.Users.Add(membership.User);
                    membership.User.UserGroups.Add(membership.UserGroup);
                }
            }

            model.Users = result.Set1;
            model.UserGroups = result.Set2;
            model.Memberships = result.Set3;

            return model;
        }

        /// <summary>
        /// Can't used stored procedures with SQLite
        /// </summary>
        /// <returns></returns>
        public Model GetSQLiteModel()
        {
            var dbUsers = DatabaseFactory.GetOpenConnection().QuerySql<User>("SELECT * FROM Users");
            var dbUserGroups = DatabaseFactory.GetOpenConnection().QuerySql<UserGroup>("SELECT * FROM UserGroup");
            var dbMemberships = DatabaseFactory.GetOpenConnection().QuerySql<Membership>("SELECT * FROM Membership");

            var model = new Model();

            var users = dbUsers.ToDictionary(x => x.ID, x => x);
            var userGroups = dbUserGroups.ToDictionary(x => x.ID, x => x);

            foreach (var membership in dbMemberships)
            {
                if (users.ContainsKey(membership.UserID) && userGroups.ContainsKey(membership.UserGroupID))
                {
                    membership.User = users[membership.UserID];
                    membership.UserGroup = userGroups[membership.UserGroupID];

                    membership.UserGroup.Users.Add(membership.User);
                    membership.User.UserGroups.Add(membership.UserGroup);
                }
            }

            model.Users = dbUsers;
            model.UserGroups = dbUserGroups;
            model.Memberships = dbMemberships;

            return model;
        }
    }
}