using System.Collections.Generic;
using NUnit.Framework;

namespace MockingDemo.Tests.BDD.UserServiceTests
{
    public class GetExistingUser : BehaviourTest
    {
        private const string GoodEmailAddress = "Laptop@asos.com";

        private UserService userService;
        private UserInfo createdUser;
        private readonly User existingUser = new UserBuilder().WithEmail(GoodEmailAddress).Build();

        protected override void Given()
        {
            var fakeUserDataService = new FakeUserDataService(new List<User>{existingUser});
            userService = new UserService(fakeUserDataService);
        }

        protected override void When()
        {
            createdUser = userService.Get(existingUser.Email);
        }

        [Then]
        public void VerifyCreatedUser()
        {
            Assert.That(createdUser, Is.EqualTo(new UserInfo(existingUser)));
        }
    }
}