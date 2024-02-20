using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using RealPlaza.Authentication.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly getAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(getAssembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(getAssembly);
       
            services.AddOptions();
            services.Configure<JWTSettings>(options => configuration.GetSection("JWTSettings").Bind(options));

        }
    }
}
