using System;
using System.Runtime.Serialization;

namespace CSVConverterLogic
{
    [Serializable]
    public class UnparseableCsvException : Exception
    {
        public UnparseableCsvException()
        {
        }

        public UnparseableCsvException(string message) : base(message)
        {
        }

        public UnparseableCsvException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnparseableCsvException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}