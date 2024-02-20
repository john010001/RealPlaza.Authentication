using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.DTOs
{
    public class AuthenticationRequest
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}
