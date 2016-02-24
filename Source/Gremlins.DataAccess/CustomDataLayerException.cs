using System;

namespace Gremlins.DataAccess
{
    [Serializable]
    public class CustomDataLayerException : DataLayerException
    {
        public CustomDataLayerException() { }
        public CustomDataLayerException(string message) : base(message) { }
        public CustomDataLayerException(string message, Exception inner) : base(message, inner) { }
        protected CustomDataLayerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
