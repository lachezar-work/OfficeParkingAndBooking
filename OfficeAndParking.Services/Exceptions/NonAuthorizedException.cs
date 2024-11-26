using System;

namespace OfficeAndParking.Services.Exceptions
{
    public class NonAuthorizedException : Exception
    {
        public NonAuthorizedException() : base() { }

        public NonAuthorizedException(string message) : base(message) { }

        public NonAuthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
