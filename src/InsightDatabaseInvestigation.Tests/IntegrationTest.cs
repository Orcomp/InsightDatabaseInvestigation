namespace InsightDatabaseInvestigation.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.DatabaseFactories;
    using InsightDatabaseInvestigation.Initializers;
    using InsightDatabaseInvestigation.Repositories;
    using NUnit.Framework;
    
    [TestFixture]
    public class IntegrationTest
    {
        private SqlDatabaseFactory _sqlDatabaseFactory;
        private SQliteDatabaseFactory _sQliteDatabaseFactory;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _sqlDatabaseFactory = new SqlDatabaseFactory();
            _sQliteDatabaseFactory = new SQliteDatabaseFactory();

            var sqlDatabaseInitializers = new IDatabaseInitializer[]
                {
                    new SqlDatabaseInitializer(_sqlDatabaseFactory),
                    new SQliteDatabaseInitializer(_sQliteDatabaseFactory, true)
                };

            foreach (var databaseInitializer in sqlDatabaseInitializers)
            {
                databaseInitializer.CreateOrUpdate();
                databaseInitializer.Seed();    
            }
        }

        [Test]
        public void TestUserRepository([ValueSource("GetUserRepositories")] IUserRepository userRepository)
        {
            var users = userRepository.GetAllUsers();
            Assert.IsNotEmpty(users);
        }

        [Test]
        public void TestModelRepository()
        {
            var modelRepository = new ModelRepository(_sqlDatabaseFactory);
            var model = modelRepository.GetModel();

            Assert.IsNotNull(model);

            Assert.True(model.UserGroups.Count == 4);
            Assert.True(model.Users.Count == 5);

            Assert.True(model.UserGroups.Single(x => x.Name == "Group1").Users.Count == 0);
            Assert.True(model.UserGroups.Single(x => x.Name == "Group2").Users.Count == 5);
            Assert.True(model.UserGroups.Single(x => x.Name == "Group3").Users.Count == 3);
            Assert.True(model.UserGroups.Single(x => x.Name == "Group4").Users.Count == 2);

            Assert.True(model.Users.All(x => x.UserGroups.Count == 2)); // All the users are in two user groups

            Assert.IsNotNull(model);
        }

        public IEnumerable<IUserRepository> GetUserRepositories()
        {
            yield return new UserRepository(new SqlDatabaseFactory(), new UserResultTransformer());
            yield return new SqliteUserRepository(new SQliteDatabaseFactory());
        }
    }
}