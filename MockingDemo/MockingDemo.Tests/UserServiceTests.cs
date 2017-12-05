using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MockingDemo.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        public const string goodEmailAddress = "Laptop@asos.com";

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User__Then_Return_The_User_Info()
        {
            var createdUser = new User(Guid.NewGuid(), "Laptop", goodEmailAddress);
            var stub = new UserDataService(createdUser);
            var sut = new UserService(stub);
            var user = sut.Get(createdUser.Email);

            Assert.That(user.Id, Is.EqualTo(createdUser.Id));
            Assert.That(user.Name, Is.EqualTo(createdUser.Name));
            Assert.That(user.Email, Is.EqualTo(createdUser.Email));
        }

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User_That_Dont_Exist__Then_Does_Not_Return_User()
        {
            var emailAddress = "NotFound@asos.com";
            var stub = new UserDataService(new User(Guid.NewGuid(), "NotFound", emailAddress));
            var sut = new UserService(stub);
            var user = sut.Get(emailAddress);
            Assert.That(user, Is.Null);
        }

        [Test]
        public void Given_An_Email_Address__When_Deleting_User__Then_User_Is_Deleted()
        {
            var mock = new DeleteUserDataService();
            var sut = new UserService(mock);
            sut.Delete("Laptop@asos.com");
            Assert.That(mock.IsDeleted, Is.True);
        }

        [Test]
        public void Given_An_Existing_User__When_Creating_User_With_Same_Email__Then_Throws_An_Exception()
        {
            var stub = new CreateUserDataService(new User(Guid.NewGuid(), "Laptop", goodEmailAddress));
            var sut = new UserService(stub);
            Assert.Throws<Exception>(() => sut.Create("Laptop", goodEmailAddress));
        }

        [Test]
        public void Given_A_New_User__When_Creating_A_User_With_An_Email_Address__Then_Return_That_User_Info()
        {
            var id = Guid.NewGuid();
            var stub = new CreateUserDataService(new User(id, "Team", "Team@asos.com"));

            GuidProvider.Current = new FakeGuidProvider(id);
            var sut = new UserService(stub);
            var userInfo = sut.Create("Team", "Team@asos.com");

            Assert.That(userInfo.Id, Is.EqualTo(id));
            Assert.That(userInfo.Name, Is.EqualTo("Team"));
            Assert.That(userInfo.Email, Is.EqualTo("Team@asos.com"));

            GuidProvider.ResetToDefault();
        }

        public class CreateUserDataService : IUserDataService
        {
            private User user;

            public CreateUserDataService(User user)
            {
                this.user = user;
            }

            public User GetByEmail(string email)
            {
                if (email == goodEmailAddress)
                {
                    return new User(this.user.Id, this.user.Name, email);
                }

                return null;
            }

            public User Create(Guid id, string name, string email)
            {
                return new User(id, name, email);
            }

            public void Delete(string email)
            {
                throw new NotImplementedException();
            }
        }


        public class DeleteUserDataService : IUserDataService
        {
            public bool IsDeleted { get; private set; }

            public User GetByEmail(string email)
            {
                throw new NotImplementedException();
            }

            public User Create(Guid id, string name, string email)
            {
                throw new NotImplementedException();
            }

            public void Delete(string email)
            {
                this.IsDeleted = true;
            }
        }


        public class UserDataService : IUserDataService
        {
            private User user;

            public UserDataService(User user)
            {
                this.user = user;
            }

            public User GetByEmail(string email)
            {
                if (email == goodEmailAddress)
                {
                    return new User(this.user.Id, this.user.Name, email);
                }

                return null;
            }

            public User Create(Guid id, string name, string email)
            {
                throw new NotImplementedException();
            }

            public void Delete(string email)
            {
                throw new NotImplementedException();
            }
        }
    }
}