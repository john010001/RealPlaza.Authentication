using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.Packaging.Ionic.Zip;
using RealPlaza.Authentication.Application.DTOs;
using RealPlaza.Authentication.Application.Persistence;
using RealPlaza.Authentication.Application.Security;
using RealPlaza.Authentication.Application.Wrappers;
using RealPlaza.Authentication.Domain.Exceptions;
using RealPlaza.Authentication.Domain.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion
{
    public class IniciarSesionHandler : IRequestHandler<IniciarSesionQuery, ResultResponse<IniciarSesionResponse?>>
    {
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly JWTSettings _jwtSettings;

        public IniciarSesionHandler( IMapper mapper, IUsuarioRepository usuarioRepository, IOptions<JWTSettings> jwtSettings
           )
        {
         
            this.mapper = mapper;
            this.usuarioRepository = usuarioRepository;
            this._jwtSettings = jwtSettings.Value;
        }

        public async Task<ResultResponse<IniciarSesionResponse?>> Handle(IniciarSesionQuery request, CancellationToken cancellationToken)
        {
            ResultResponse<IniciarSesionResponse> result = new ResultResponse<IniciarSesionResponse>();

            var userRequest = mapper.Map<AuthenticationRequest?>(request);
            string password = userRequest.Password;

            var passwordEncriptado = Encryptor.Sha1(request.Password);
            userRequest.Password = passwordEncriptado;
            var usuario = await usuarioRepository.ObtenerUsuario(userRequest.Correo);


            if (usuario == null)
            {
                throw new Exception($"Usuario o contraseña incorrecta.");
            }
            else
            {

                if (_jwtSettings == null || string.IsNullOrEmpty(_jwtSettings.Key))
                {
                    throw new ArgumentNullException(nameof(_jwtSettings), "La propiedad Key de _jwtSettings no está inicializada.");
                }

                JwtSecurityToken jwtSecurityToken = GenerateJWToken(usuario);
                usuario.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                // Asumiendo que IniciarSesionResponse tiene una propiedad 'Correo'
                var iniciarSesionResponse = new IniciarSesionResponse { Correo = usuario.Correo , JWToken = usuario.JWToken };

                result.Data = iniciarSesionResponse;
                result.Message = $"Usuario autenticado {usuario.Correo}";

                return result;
            }

        }


        private JwtSecurityToken GenerateJWToken(IniciarSesionResponse user)
        {
            var roleClaims = new List<Claim>();
            roleClaims.Add(new Claim("roles", "Administrador"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Correo),
             
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddYears(3),//DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }


    }
}
