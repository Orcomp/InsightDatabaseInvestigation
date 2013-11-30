namespace InsightDatabaseInvestigation.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class UserGroup
    {
        public UserGroup()
        {
            Users = new Collection<User>();
        }

        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}