using Xunit.Sdk;

namespace PotatoServerTestsCore.Exceptions
{
    class PotatoEqualException : EqualException
    {
        public override string Message => $"\r\n{base.Message} \r\nReturns: {UserMessage}";

        public PotatoEqualException(object expected, object actual, string errorMessage) : base(expected, actual)
        {
            UserMessage = errorMessage;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
