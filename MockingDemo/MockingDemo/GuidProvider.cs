using System;

namespace MockingDemo
{
    public abstract class GuidProvider
    {
        private static GuidProvider current;

        static GuidProvider()
        {
            GuidProvider.current = new DefaultGuidProvider();
        }

        public static GuidProvider Current
        {
            get { return GuidProvider.current; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                GuidProvider.current = value;
            }
        }

        public abstract Guid Id { get; }

        public static void ResetToDefault()
        {
            GuidProvider.current = new DefaultGuidProvider();
        }
    }
}