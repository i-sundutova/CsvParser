using System;
using System.Runtime.Serialization;

namespace MI.CsvParser.Exceptions
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException()
        {
        }

        protected InvalidFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidFileException(string message) : base(message)
        {
        }

        public InvalidFileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
