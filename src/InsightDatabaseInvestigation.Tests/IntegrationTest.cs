namespace InsightDatabaseInvestigation.Tests
{
    using System.Collections.Generic;
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
        public void TestUserRepository([ValueSource("GetUserRepositories")]IUserRepository userRepository)
        {
            var users = userRepository.GetAllUsers();
            Assert.IsNotEmpty(users);
        }

        public IEnumerable<IUserRepository> GetUserRepositories()
        {
            yield return new UserRepository(new SqlDatabaseFactory(), new UserResultTransformer());
            yield return new SqliteUserRepository(new SQliteDatabaseFactory());
        }
    }
}