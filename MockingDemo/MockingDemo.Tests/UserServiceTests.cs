using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace MockingDemo.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserDataService fakeUserDataService;
        private UserService userService;
        public const string goodEmailAddress = "Laptop@asos.com";

        [SetUp]
        public void Setup()
        {
            fakeUserDataService = Substitute.For<IUserDataService>();
            userService = new UserService(fakeUserDataService);
        }

        [TearDown]
        public void Teardown()
        {
            GuidProvider.ResetToDefault();
        }

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User__Then_Return_The_User_Info()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            this.fakeUserDataService.GetByEmail(goodEmailAddress).Returns(existingUser);

            var user = this.userService.Get(existingUser.Email);

            Assert.That(user, Is.EqualTo(new UserInfo(existingUser)));
        }

        [Test]
        public void Given_An_Email_Address__When_Getting_A_User_That_Dont_Exist__Then_Does_Not_Return_User()
        {
            var nonExistentUser = new UserBuilder().WithEmail("notfound@asos.com").Build();
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();
            fakeUserDataService.GetByEmail(goodEmailAddress).Returns(existingUser);

            var user = this.userService.Get(nonExistentUser.Email);

            Assert.That(user, Is.Null);
        }

        [Test]
        public void Given_An_Email_Address__When_Deleting_User__Then_User_Is_Deleted()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();

            this.userService.Delete(goodEmailAddress);

            this.fakeUserDataService.Received().Delete(existingUser.Email);
        }

        [Test]
        public void Given_An_Existing_User__When_Creating_User_With_Same_Email__Then_Throws_An_Exception()
        {
            var existingUser = new UserBuilder().WithEmail(goodEmailAddress).Build();

            fakeUserDataService.GetByEmail(goodEmailAddress).Returns(existingUser);

            Assert.Throws<Exception>(() => this.userService.Create(existingUser.Name, existingUser.Email));
        }

        [Test]
        public void Given_A_New_User__When_Creating_A_User_With_An_Email_Address__Then_Return_That_User_Info()
        {
            var id = Guid.NewGuid();
            GuidProvider.Current = new FakeGuidProvider(id);

            var expectedUser = new UserBuilder().WithId(id).WithEmail(goodEmailAddress).Build();

            fakeUserDataService.GetByEmail(goodEmailAddress).Returns((User)null);
            fakeUserDataService.Create(expectedUser.Id, expectedUser.Name, expectedUser.Email).Returns(expectedUser);

            var createdUser = this.userService.Create(expectedUser.Name, expectedUser.Email);

            Assert.That(createdUser, Is.EqualTo(new UserInfo(expectedUser)));
        }
    }
}