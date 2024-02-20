using Dapper;
using RealPlaza.Authentication.Application.Features.Login.Queries.IniciarSesion;
using RealPlaza.Authentication.Application.Persistence;
using RealPlaza.Authentication.Application.Security;
using RealPlaza.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public UsuarioRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        public async Task<bool> GuardarUsuario(Usuario usuario)
        {
            string str = Encryptor.RandomString(4);
            string password = Encryptor.Sha1(usuario.Password);
            usuario.FechaRegistro = DateTime.Now;

            DynamicParameters parameters = new();

            parameters.Add("@CORREO", usuario.Correo);
            parameters.Add("@PASSWORD", password);
            parameters.Add("@FECHAREGISTRO", usuario.FechaRegistro);

            await unitOfWork.Connection.ExecuteAsync("sp_GuardarUsuarioAppRealPlaza", param: parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

      

        public async Task<IniciarSesionResponse?> ObtenerUsuario(string correo)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Correo", correo);

            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<IniciarSesionResponse>("sp_ObtenerUsuarioCorreo",
                param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<Usuario?> ObtenerUsuario(string correo, string password)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Correo", correo);
            parameters.Add("@Password", password);

            return await unitOfWork.ConnectionSecurity.QueryFirstOrDefaultAsync<Usuario>("sp_LoginAppMiCuenta",
                param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
