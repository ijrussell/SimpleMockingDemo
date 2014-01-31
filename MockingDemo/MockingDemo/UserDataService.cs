using System;

namespace MockingDemo
{
    public class UserDataService : IUserDataService
    {
        public User GetByEmail(string email)
        {
            return null;
        }

        public User Create(Guid id, string name, string email)
        {
            return null;
        }

        public void Delete(string email)
        {
            //Do nothing
        }
    }
}