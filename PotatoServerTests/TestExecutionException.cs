using System;

namespace ListNestTests
{
    internal class TestExecutionException : Exception
    {
        public TestExecutionException()
        {
        }

        public TestExecutionException(string message) : base(message)
        {
        }

        public TestExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
