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
            var modelRepository = new ModelRepository(DatabaseFactory);
            var model = modelRepository.GetSQLiteModel();

            return model.Users;
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