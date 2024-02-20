using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Detail { get; set; }
        public NotFoundException() { }
        public NotFoundException(string message) { }
        public NotFoundException(string message, Exception innerException) { }
        public NotFoundException(string title, string detail) : base(title) => Detail = detail;
    }
}
