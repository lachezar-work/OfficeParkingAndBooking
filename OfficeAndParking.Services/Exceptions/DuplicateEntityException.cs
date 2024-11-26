using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OfficeAndParking.Services.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public string StatusCode { get; }

        public DuplicateEntityException() : base() { }

        public DuplicateEntityException(string message) : base(message) { }

        public DuplicateEntityException(string message, string statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }

        public DuplicateEntityException(string message, string statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
