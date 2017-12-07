using System.Collections.Generic;
using NUnit.Framework;

namespace MockingDemo.Tests.BDD.UserServiceTests
{
    public class GetUnknownUser : BehaviourTest
    {
        private const string GoodEmailAddress = "Laptop@asos.com";

        private UserService userService;
        private UserInfo createdUser;

        protected override void Given()
        {
            var fakeUserDataService = new FakeUserDataService(new List<User>());
            userService = new UserService(fakeUserDataService);
        }

        protected override void When()
        {
            createdUser = userService.Get(GoodEmailAddress);
        }

        [Then]
        public void VerifyCreatedUser()
        {
            Assert.That(createdUser, Is.Null);
        }
    }
}