namespace InsightDatabaseInvestigation.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class UserGroup
    {
        public UserGroup()
        {
            Users = new Collection<User>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return string.Format("UserGroupID: {0}, Name: {1}, Users: {2}", ID, Name, string.Join(", ", Users.Select(x => string.Format("{0} {1}", x.FirstName, x.LastName))));
        }
    }
}