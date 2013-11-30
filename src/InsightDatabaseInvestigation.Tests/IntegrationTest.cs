using InsightDatabaseInvestigation.DatabaseFactories;
using InsightDatabaseInvestigation.Initializers;
using InsightDatabaseInvestigation.Repositories;
using NUnit.Framework;

namespace InsightDatabaseInvestigation.Tests
{
    [TestFixture]
    public class IntegrationTest
    {
        private SqlDatabaseFactory _sqlDatabaseFactory;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _sqlDatabaseFactory =    new SqlDatabaseFactory();
            var sqlDatabaseInitializer = new SqlDatabaseInitializer(_sqlDatabaseFactory);

            sqlDatabaseInitializer.CreateOrUpdate();
            sqlDatabaseInitializer.Seed();
        }

        [Test]
        public void TestUserRepository()
        {
            var userRepository = new UserRepository(_sqlDatabaseFactory, new UserResultTransformer());
            var users = userRepository.GetAllUsers();

            Assert.IsNotEmpty(users);
        }
    }
}