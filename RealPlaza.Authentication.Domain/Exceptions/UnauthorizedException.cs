using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class UnauthorizedException : Exception 
    {
        public string Detail { get; set; }
        public UnauthorizedException() { }
        public UnauthorizedException(string message) { }
        public UnauthorizedException(string message, Exception innerException) { }
        public UnauthorizedException(string title, string detail) : base(title) => Detail = detail;
    }
}
