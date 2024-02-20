
using MediatR;
using RealPlaza.Authentication.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion
{
    public record IniciarSesionQuery(string Correo, string Password) : IRequest<ResultResponse<IniciarSesionResponse?>>;
}
