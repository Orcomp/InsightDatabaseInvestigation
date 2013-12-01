using InsightDatabaseInvestigation.DatabaseFactories;
using InsightDatabaseInvestigation.Initializers;
using InsightDatabaseInvestigation.Repositories;
using NUnit.Framework;

namespace InsightDatabaseInvestigation.Tests
{
    using System.Linq;

    [TestFixture]
    public class IntegrationTest
    {
        private SqlDatabaseFactory _sqlDatabaseFactory;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _sqlDatabaseFactory = new SqlDatabaseFactory();
            var sqlDatabaseInitializer = new SqlDatabaseInitializer(_sqlDatabaseFactory);

            sqlDatabaseInitializer.CreateOrUpdate();
            sqlDatabaseInitializer.Seed();
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
    }
}