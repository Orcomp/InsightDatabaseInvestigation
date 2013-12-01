namespace InsightDatabaseInvestigation.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class User
    {
        public User()
        {
            UserGroups = new Collection<UserGroup>();
        }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public override string ToString()
        {
            return string.Format("UserID: {0}, FirstName: {1}, LastName: {2}, UserGroups: {3}", UserID, FirstName, LastName, string.Join(", ", UserGroups.Select(x => x.Name)));
        }
    }
}