using System;
using System.Collections.Generic;
using System.Linq;

namespace MockingDemo.Tests
{
    public class FakeUserDataService : IUserDataService
    {
        private readonly List<User> users;

        public FakeUserDataService(List<User> users)
        {
            this.users = users;
        }

        public User GetByEmail(string email)
        {
            return users.SingleOrDefault(x => x.Email == email);
        }

        public User Create(Guid id, string name, string email)
        {
            this.users.Add(new User(id, name, email));

            return GetByEmail(email);
        }

        public void Delete(string email)
        {
            var user = GetByEmail(email);

            if (user != null)
            {
                this.users.Remove(user);
            }
        }
    }
}
