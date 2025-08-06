using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _conexion;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _conexion = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            string sqlQuery = @"sp_Usuario_Crear";

            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(sqlQuery, new 
                {
                    idUsuario = Guid.NewGuid(),
                    nombre = usuario.Nombre,
                    primerApellido = usuario.PrimerApellido,
                    segundoApellido = usuario.SegundoApellido,
                    email = usuario.Email,
                    password = usuario.Password,
                    monedaPrincipal = usuario.MonedaPrincipal,
                    fechaCreacion = DateTime.Now,
                    fechaUltimoAcceso = DateTime.Now,
                    estado = usuario.Estado
                }
            );

            return resultadoQuery;
        }

        public async Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario)
        {
            await VerificarUsuario(id);

            string sqlQuery = @"sp_Usuario_Editar";

            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(sqlQuery, new
                {
                    idUsuario = id,
                    nombre = usuario.Nombre,
                    primerApellido = usuario.PrimerApellido,
                    segundoApellido = usuario.SegundoApellido,
                    email = usuario.Email,
                    password = usuario.Password,
                    monedaPrincipal = usuario.MonedaPrincipal,
                    fechaUltimoAcceso = DateTime.Now,
                    estado = usuario.Estado
                }
            );

            return resultadoQuery;
        }

        public async Task<Guid> EliminarUsuario(Guid id)
        {
            await VerificarUsuario(id);

            string sqlQuery = @"sp_Usuario_Eliminar";

            var resultadoQuery = await _conexion.ExecuteScalarAsync<Guid>(sqlQuery, new { idUsuario = id } );

            return resultadoQuery;
        }
        public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios()
        {
            string sqlQuery = @"sp_Usuario_ObtenerTodos";

            var resultadoQuery = await _conexion.QueryAsync<UsuarioResponse>(sqlQuery);

            return resultadoQuery;
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            string sqlQuery = @"sp_Usuario_ObtenerPorId";

            var resultadoQuery = await _conexion.QuerySingleOrDefaultAsync<UsuarioResponse>(sqlQuery, new { idUsuario = id });

            return resultadoQuery;
        }

        private async Task VerificarUsuario(Guid id)
        {
            var resultadoVerificacion = await ObtenerUsuarioPorId(id);

            if (resultadoVerificacion == null)
            {
                throw new Exception("El usuario no existe.");
            }
        }
    }
}
