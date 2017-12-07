using System;

namespace MockingDemo.Tests
{
    public class UserBuilder
    {
        private Guid id;
        private string name;
        private string email;

        public UserBuilder()
        {
            this.id = Guid.NewGuid();
            this.name = "Default name";
            this.email = "default@asos.com";
        }

        public UserBuilder WithId(Guid id)
        {
            this.id = id;
            return this;
        }

        public UserBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            this.email = email;
            return this;
        }

        public User Build()
        {
            return new User(this.id, this.name, this.email);
        }
    }
}