using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Domain.Entities
{
    public class Usuario
    {
        public string? Correo { get; set; } 
        public string? Password { get; set; } 
        public DateTime? FechaRegistro { get; set; } 
    }
}
