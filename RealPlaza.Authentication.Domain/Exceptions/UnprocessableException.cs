using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class UnprocessableException : Exception
    {
        public string Detail { get; set; }
        public UnprocessableException() { }
        public UnprocessableException(string message) { }
        public UnprocessableException(string message, Exception innerException) { }
        public UnprocessableException(string title, string detail) : base(title) => Detail = detail;
    }
}
