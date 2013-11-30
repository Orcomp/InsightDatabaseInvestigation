namespace InsightDatabaseInvestigation.Contract
{
    using System.Collections.Generic;
    using InsightDatabaseInvestigation.Model;

    public interface IUserRepository
    {
        IList<User> GetAllUsers();
        User GetUserById(int id);
        IList<User> GetTopUsers(int count);
    }
}