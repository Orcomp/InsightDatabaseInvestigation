namespace InsightDatabaseInvestigation.Model
{
    public class Membership
    {
        public int MembershipID { get; set; }

        public int GroupID { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}