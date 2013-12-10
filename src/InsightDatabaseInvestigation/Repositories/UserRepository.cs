namespace InsightDatabaseInvestigation.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Insight.Database;
    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.Model;

    public class UserRepository : IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory, IResultTransformer<User> resultTransformer)
        {
            DatabaseFactory = databaseFactory;
            ResultTransformer = resultTransformer;
        }

        public IDatabaseFactory DatabaseFactory { get; set; }
        public IResultTransformer<User> ResultTransformer { get; set; }

        public IList<User> GetAllUsers()
        {
            string commandText = @"GetModel";
            var res = DatabaseFactory.GetOpenConnection().QueryResults<User, UserGroup, Membership>(commandText);
            return ResultTransformer.Flatten(res);
        }

        public User GetUserById(int id)
        {
            string commandText = @"GetUserById";
            var res = DatabaseFactory.GetOpenConnection().QueryResults<User, UserGroup, Membership>(commandText);
            return ResultTransformer.Flatten(res).SingleOrDefault();
        }

        public IList<User> GetTopUsers(int count)
        {
            string commandText = @"GetTopUsers";
            var res = DatabaseFactory.GetOpenConnection().QueryResults<User, UserGroup, Membership>(commandText, count);
            return ResultTransformer.Flatten(res);
        } 
    }
}