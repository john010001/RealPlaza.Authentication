using RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion;
using RealPlaza.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Persistence
{
    public interface IUsuarioRepository
    {
        Task<bool> GuardarUsuario(Usuario usuario);
        Task<Usuario?> ObtenerUsuario(string correo, string password);

        Task<IniciarSesionResponse?> ObtenerUsuario(string correo);
    }
}
