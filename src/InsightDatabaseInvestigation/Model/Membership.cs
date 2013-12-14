namespace InsightDatabaseInvestigation.Model
{
    public class Membership
    {
        public int ID { get; set; }

        public int UserGroupID { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
        public UserGroup UserGroup { get; set; }

        public override string ToString()
        {
            return string.Format("MembershipID: {0}, User: {1}, UserGroup: {2}", ID, string.Format("{0}: {1}", User.ID, User.FirstName + " " + User.LastName), string.Format("{0}: {1}", UserGroup.ID, UserGroup.Name));
        }
    }
}