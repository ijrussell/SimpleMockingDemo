using NSubstitute.Core;
using NUnit.Framework;

namespace MockingDemo.Tests.BDD
{
    [TestFixture]
    public abstract class BehaviourTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();
    }
}