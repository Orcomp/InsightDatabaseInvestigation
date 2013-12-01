namespace InsightDatabaseInvestigation.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Model
    {
        public IList<Membership> Memberships { get; set; }
        public IList<User> Users { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
    }
}