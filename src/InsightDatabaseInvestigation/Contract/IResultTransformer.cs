namespace InsightDatabaseInvestigation.Contract
{
    using System.Collections.Generic;
    using Insight.Database;

    public interface IResultTransformer<T>
    {
        IList<T> Flatten(Results<T> result);
    }
}