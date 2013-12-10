namespace InsightDatabaseInvestigation.Repositories
{
    using System.Data;
    using System.Linq;    
    using System.Collections.Generic;
    using Insight.Database;
    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.Model;

    public class SqliteUserRepository : IUserRepository
    {
        public SqliteUserRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }        
        
        public IDatabaseFactory DatabaseFactory { get; set; }

        public IList<User> GetAllUsers()
        {
            using (var connection = DatabaseFactory.GetOpenConnection())
            {
                var userQuery = "Select * from [Users]";
                var userGroupQuery = "Select * from [UserGroup]";
                var membershipQuery = "Select * from [Membership]";

                var users = connection.Query<User>(userQuery, commandType: CommandType.Text);
                var userGroups = connection.Query<UserGroup>(userGroupQuery, commandType: CommandType.Text);
                var memberships = connection.Query<Membership>(membershipQuery, commandType: CommandType.Text);

                foreach (var membership in memberships)
                {
                    membership.User = users.First(x => x.UserID == membership.UserID);
                    membership.UserGroup = userGroups.First(x => x.UserGroupID == membership.UserGroupID);
                    membership.UserGroup.Users.Add(membership.User);
                    membership.User.UserGroups.Add(membership.UserGroup);
                }
                return users;
            }
        }

        public User GetUserById(int id)
        {
            return null;
        }

        public IList<User> GetTopUsers(int count)
        {
            return null;
        }
    }
}