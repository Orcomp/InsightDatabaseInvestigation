namespace InsightDatabaseInvestigation.Contract
{
    using System.Collections.Generic;
    using Insight.Database;

    public abstract class ResultTransformer<T, TU> : IResultTransformer<T>
        where TU : Results<T>
    {
        public abstract IList<T> Flatten(Results<T> result);
    }
}