using MediatR;
using RealPlaza.Authentication.Application.Wrappers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Features.Login.Commands.Registrar
{
    public record RegistrarCommand(string correo, string Password,DateTime fechaRegistro) : IRequest<ResultResponse<bool>>;
}
