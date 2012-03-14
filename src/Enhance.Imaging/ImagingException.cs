using System;
using System.Runtime.Serialization;

namespace Enhance.Imaging
{
    [Serializable]
    public class ImagingException : Exception
    {
        public ImagingException()
        {
        }

        public ImagingException(string message)
            : base(message)
        {
        }

        public ImagingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ImagingException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}