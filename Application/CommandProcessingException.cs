using System;

namespace Application.Core
{
    public class CommandProcessingException : Exception
    {
        public CommandProcessingException()
        {

        }

        public CommandProcessingException(string message) : base(message)
        {

        }

        public CommandProcessingException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
