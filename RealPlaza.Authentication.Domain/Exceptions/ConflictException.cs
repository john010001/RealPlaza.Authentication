using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class ConflictException : Exception
    {
        public string Detail { get; set; }
        public ConflictException() { }
        public ConflictException(string message) { }
        public ConflictException(string message, Exception innerException) { }
        public ConflictException(string title, string detail) : base(title) => Detail = detail;
    }
}
