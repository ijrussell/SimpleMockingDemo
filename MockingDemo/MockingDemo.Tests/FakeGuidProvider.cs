using System;

namespace MockingDemo.Tests
{
    public class FakeGuidProvider : GuidProvider
    {
        private readonly Guid _id;

        public FakeGuidProvider(Guid id)
        {
            _id = id;
        }

        public override Guid Id
        {
            get { return _id; }
        }
    }
}