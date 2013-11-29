namespace InsightDatabaseInvestigation.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class User
    {
        public User()
        {
            UserGroups = new Collection<UserGroup>();
        }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}