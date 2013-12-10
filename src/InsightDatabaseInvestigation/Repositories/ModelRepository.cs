namespace InsightDatabaseInvestigation.Repositories
{
    using System.Collections.Generic;
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

            var users = result.Set1.ToDictionary(x => x.UserID, x => x);
            var userGroups = result.Set2.ToDictionary(x => x.UserGroupID, x => x);

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
    }
}