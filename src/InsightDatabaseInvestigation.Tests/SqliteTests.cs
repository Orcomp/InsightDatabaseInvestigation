namespace InsightDatabaseInvestigation.Tests
{
    using System.Data;
    using InsightDatabaseInvestigation.DatabaseFactories;
    using NUnit.Framework;

    [TestFixture]
    public class SqliteTests
    {
        [Test]
        public void TestSqliteWithoutODBC()
        {
            var factory = new SQliteDatabaseFactory();
            var connection = factory.GetOpenConnection();
            
            Assert.AreEqual(ConnectionState.Open, connection.State);
        }
    }
}