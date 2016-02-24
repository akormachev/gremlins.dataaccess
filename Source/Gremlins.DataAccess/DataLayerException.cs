using System;
using System.Runtime.Serialization;

namespace Gremlins.DataAccess
{
    public class DataLayerException : Exception
    {
        public DataLayerException()
            : base()
        { }

        public DataLayerException(string message)
            : base(message)
        { }

        public DataLayerException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DataLayerException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        { }
    }
}

