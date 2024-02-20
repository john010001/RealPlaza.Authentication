using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealPlaza.Authentication.Application.Persistence;
using RealPlaza.Authentication.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Infrastructure
{
    public static class InfraestructureServiceRegistration
    {
        public static void AddInfraestructureService(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddScoped<IUnitOfWork>(options =>
            {
                return new UnitOfWork(
                    new SqlConnection(configuration.GetConnectionString("DefaultConnection")),
                    new SqlConnection(configuration.GetConnectionString("SecurityConnection")));
            });

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        }
    }
}
