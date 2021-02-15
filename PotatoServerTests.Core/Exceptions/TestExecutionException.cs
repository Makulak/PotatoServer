using System;

namespace PotatoServerTestsCore.Exceptions
{
    public class TestExecutionException : Exception
    {
        public override string Message => $"\r\n {base.Message}";
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
