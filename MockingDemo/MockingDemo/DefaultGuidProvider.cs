using System;

namespace MockingDemo
{
    public class DefaultGuidProvider : GuidProvider
    {
        public override Guid Id
        {
            get { return Guid.NewGuid(); }
        }
    }
}