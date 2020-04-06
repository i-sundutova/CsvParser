using System;
using System.Runtime.Serialization;

namespace MI.CsvParser.Exceptions
{
    public class ConvertationException : Exception
    {
        public ConvertationException()
        {
        }

        protected ConvertationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConvertationException(string message) : base(message)
        {
        }

        public ConvertationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
