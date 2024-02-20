using AutoMapper;
using RealPlaza.Authentication.Application.DTOs;
using RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion;
using RealPlaza.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<IniciarSesionQuery, AuthenticationRequest>().ReverseMap();
        }

        
    }
}
