namespace InsightDatabaseInvestigation.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Insight.Database;
    using InsightDatabaseInvestigation.Contract;
    using InsightDatabaseInvestigation.Model;

    public class UserResultTransformer : ResultTransformer<User, Results<User, UserGroup, Membership>>
    {
        private IList<User> Flatten(Results<User, UserGroup, Membership> result)
        {
            foreach (var membership in result.Set3)
            {
                membership.User = result.Set1.First(x => x.UserID == membership.UserID);
                membership.UserGroup = result.Set2.First(x => x.GroupID == membership.GroupID);
                membership.UserGroup.Users.Add(membership.User);
                membership.User.UserGroups.Add(membership.UserGroup);
            }

            return result.Set1;
        }

        public override IList<User> Flatten(Results<User> result)
        {
            if (result is Results<User, UserGroup, Membership>)
            {
                return Flatten(result as Results<User, UserGroup, Membership>);
            }

            // default
            return result.Set1;
        }
    }
}