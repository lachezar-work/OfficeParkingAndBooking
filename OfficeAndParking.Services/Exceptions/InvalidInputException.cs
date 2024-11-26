using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OfficeAndParking.Services.Exceptions
{
    public class InvalidInputException : Exception
    {
        public string StatusCode { get; }

        public InvalidInputException() : base() { }

        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, string statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public InvalidInputException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidInputException(string message, string statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
