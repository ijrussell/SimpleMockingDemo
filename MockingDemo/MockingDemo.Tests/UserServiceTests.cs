using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MockingDemo.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        public const string goodEmailAddress = "Laptop@asos.com";

        [TearDown]
        public void Teardown()
        {
            GuidProvider.ResetToDefault();
        }

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User__Then_Return_The_User_Info()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            var fakeUserDataService = new FakeUserDataService(BuildUserList(existingUser));
            var sut = new UserService(fakeUserDataService);
            var user = sut.Get(existingUser.Email);

            Assert.That(user, Is.EqualTo(new UserInfo(existingUser)));
        }

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User_That_Dont_Exist__Then_Does_Not_Return_User()
        {
            var nonExistentUser = new UserBuilder().WithEmail("notfound@asos.com").Build();
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            var fakeUserDataService = new FakeUserDataService(BuildUserList(existingUser));
            var sut = new UserService(fakeUserDataService);

            var user = sut.Get(nonExistentUser.Email);

            Assert.That(user, Is.Null);
        }

        [Test]
        public void Given_An_Email_Address__When_Deleting_User__Then_User_Is_Deleted()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            var fakeUserDataService = new FakeUserDataService(BuildUserList(existingUser));
            var sut = new UserService(fakeUserDataService);

            sut.Delete(goodEmailAddress);

            Assert.That(fakeUserDataService.GetByEmail(existingUser.Email), Is.Null);
        }

        [Test]
        public void Given_An_Existing_User__When_Creating_User_With_Same_Email__Then_Throws_An_Exception()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            var fakeUserDataService = new FakeUserDataService(BuildUserList(existingUser));
            var sut = new UserService(fakeUserDataService);

            Assert.Throws<Exception>(() => sut.Create(existingUser.Name, existingUser.Email));
        }

        [Test]
        public void Given_A_New_User__When_Creating_A_User_With_An_Email_Address__Then_Return_That_User_Info()
        {
            var id = Guid.NewGuid();
            var expectedUser = new UserBuilder().WithId(id).WithEmail(goodEmailAddress).Build();
            var fakeUserDataService = new FakeUserDataService(new List<User>());

            GuidProvider.Current = new FakeGuidProvider(id);
            var sut = new UserService(fakeUserDataService);

            var createdUser = sut.Create(expectedUser.Name, expectedUser.Email);

            Assert.That(createdUser, Is.EqualTo(new UserInfo(expectedUser)));
        }

        private List<User> BuildUserList(User user)
        {
            return new List<User>
            {
                user
            };
        }

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
}