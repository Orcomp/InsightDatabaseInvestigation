namespace InsightDatabaseInvestigation.Initializers
{
    using System.Collections.Generic;
    using InsightDatabaseInvestigation.Model;

    public class UniqueMembershipEqualityProvider : IEqualityComparer<Membership>
    {
        public bool Equals(Membership x, Membership y)
        {
            return x.UserGroupID == y.UserGroupID && x.UserID == y.UserID;
        }

        public int GetHashCode(Membership obj)
        {
            return obj.UserGroupID.GetHashCode() + obj.UserID.GetHashCode();
        }
    }
}