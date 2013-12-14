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
            var users = result.Set1.ToDictionary(x => x.ID, x => x);
            var userGroups = result.Set2.ToDictionary(x => x.ID, x => x);

            foreach (var membership in result.Set3)
            {
                if (users.ContainsKey(membership.UserID) && userGroups.ContainsKey(membership.UserGroupID))
                {
                    membership.User = users[membership.UserID];
                    membership.UserGroup = userGroups[membership.UserGroupID];

                    membership.UserGroup.Users.Add(membership.User);
                    membership.User.UserGroups.Add(membership.UserGroup);
                }
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