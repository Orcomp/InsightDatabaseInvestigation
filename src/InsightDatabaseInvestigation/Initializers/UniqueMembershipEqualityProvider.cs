namespace InsightDatabaseInvestigation.Initializers
{
    using System.Collections.Generic;
    using InsightDatabaseInvestigation.Model;

    public class UniqueMembershipEqualityProvider : IEqualityComparer<Membership>
    {
        public bool Equals(Membership x, Membership y)
        {
            return x.GroupID == y.GroupID && x.UserID == y.UserID;
        }

        public int GetHashCode(Membership obj)
        {
            return obj.GroupID.GetHashCode() + obj.UserID.GetHashCode();
        }
    }
}