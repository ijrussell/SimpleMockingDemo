using System;
using NSubstitute;
using NUnit.Framework;

namespace MockingDemo.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void get_returns_an_existing_user()
        {
            var id = Guid.NewGuid();
            var name = "Test";

            var stub = new UserReturnedUserDataService(id, name);

            var sut = new UserService(stub);

            var email = "test@test.com";

            var result = sut.Get(email);

            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
        }

        [Test]
        public void get_returns_an_non_existing_user()
        {
            var stub = new NoUserReturnedUserDataService();

            var sut = new UserService(stub);

            var email = "test@test.com";

            var result = sut.Get(email);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void user_who_doesnt_exist_is_created()
        {
            var stub = new NoUserReturnedAndCreateUserUserDataService();

            var sut = new UserService(stub);

            var id = Guid.NewGuid();

            GuidProvider.Current = new FakeGuidProvider(id);

            var name = "test";
            var email = "test@test.com";

            var result = sut.Create(name, email);

            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Id, Is.EqualTo(id));

            GuidProvider.ResetToDefault();
        }

        [Test]
        public void user_who_does_exist_is_not_created()
        {
            var name = "Test";
            var email = "test@test.com";
            
            var stub = Substitute.For<IUserDataService>();

            stub.GetByEmail(email).Returns(new User(Guid.NewGuid(), name, email));

            var sut = new UserService(stub);

            Assert.Throws<Exception>(() => sut.Create(name, email));
        }

        [Test]
        public void user_is_deleted()
        {
            var mock = Substitute.For<IUserDataService>();
            
            var email = "test@test.com";

            var sut = new UserService(mock);

            sut.Delete(email);

            mock.Received().Delete(email);
        }

        public class UserReturnedUserDataService : IUserDataService
        {
            private readonly Guid _id;
            private readonly string _name;

            public UserReturnedUserDataService()
            {

            }

            public UserReturnedUserDataService(Guid id, string name)
            {
                _id = id;
                _name = name;
            }

            public User GetByEmail(string email)
            {
                return new User(_id, _name, email);
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

        public class NoUserReturnedUserDataService : IUserDataService
        {
            public User GetByEmail(string email)
            {
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

        public class NoUserReturnedAndCreateUserUserDataService : IUserDataService
        {
            public Guid Id { get; set; }

            public User GetByEmail(string email)
            {
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

        public class UserDeletedUserUserDataService : IUserDataService
        {
            public bool DeleteWasCalled { get; set; }
            public string Email { get; set; }

            public User GetByEmail(string email)
            {
                return null;
            }

            public User Create(Guid id, string name, string email)
            {
                return new User(id, name, email);
            }

            public void Delete(string email)
            {
                Email = email;
                DeleteWasCalled = true;
            }
        }
    }
}