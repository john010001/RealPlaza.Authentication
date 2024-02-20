using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Exceptions
{
    public class GeneralException : Exception
    {
        public string Detail { get; set; }
        public GeneralException() { }
        public GeneralException(string message) { }
        public GeneralException(string message, Exception innerException) { }
        public GeneralException(string title, string detail) : base(title) => Detail = detail;
    }
}
