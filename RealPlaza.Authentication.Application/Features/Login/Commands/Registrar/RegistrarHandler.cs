using AutoMapper;
using FluentValidation;
using MediatR;
using RealPlaza.Authentication.Application.Persistence;
using RealPlaza.Authentication.Application.Wrappers;
using RealPlaza.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Features.Login.Commands.Registrar
{
    public class RegistrarHandler : IRequestHandler<RegistrarCommand, ResultResponse<bool>>
    {
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IValidator<RegistrarCommand> validator;

        public RegistrarHandler(IMapper mapper,
            IUsuarioRepository usuarioRepository, IValidator<RegistrarCommand> validator)
        {
            this.mapper = mapper;
            this.usuarioRepository = usuarioRepository;
            this.validator = validator;
        }

        async Task<ResultResponse<bool>> IRequestHandler<RegistrarCommand, ResultResponse<bool>>.Handle(RegistrarCommand request, CancellationToken cancellationToken)
        {
            ResultResponse<bool> result = new ResultResponse<bool>();

            //var validationResult = await validator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    result.Data = false;
            //    result.Message = "Error de validación";
            //    return result;
            //}

            try
            {
                bool registroExitoso = await usuarioRepository.GuardarUsuario(new Usuario
                {
                    Correo = request.correo,
                    Password = request.Password,
                    FechaRegistro = request.fechaRegistro
                });

                if (registroExitoso)
                {
                    result.Data = true;
                    result.Message = "Registro exitoso";
                }
                else
                {
                    result.Data = false;
                    result.Message = "Error al guardar el usuario";
                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
    }
}
