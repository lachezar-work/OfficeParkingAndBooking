using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OfficeAndParking.Services.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public int StatusCode { get; }

        public DuplicateEntityException() : base() { }

        public DuplicateEntityException(string message) : base(message) { }

        public DuplicateEntityException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }

        public DuplicateEntityException(string message, int statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
