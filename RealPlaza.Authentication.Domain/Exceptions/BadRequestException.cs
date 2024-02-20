using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Detail { get; set; }
        public BadRequestException() { }
        public BadRequestException(string message) { }
        public BadRequestException(string message, Exception innerException) { }
        public BadRequestException(string title, string detail) : base(title) => Detail = detail;
    }
}
