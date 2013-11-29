namespace InsightDatabaseInvestigation.Contract
{
    public interface IDatabaseInitializer
    {
        void CreateOrUpdate();
        void Seed();
    }
}