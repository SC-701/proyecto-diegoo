using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace DA
{
    public class CuentaDA : ICuentaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CuentaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearCuenta(CuentaRequest cuenta)
        {
            string sqlQuery = @"sp_Cuenta_Crear";

            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
                {
                    idCuenta = Guid.NewGuid(),
                    idUsuario = cuenta.IdUsuario,
                    nombre = cuenta.Nombre,
                    descripcion = cuenta.Descripcion,
                    permitirSalarioNegativo = cuenta.PermitirSalarioNegativo,
                    fechaCreacion = DateTime.Now,
                    fechaUltimaModificacion = DateTime.Now,
                    estado = cuenta.Estado,
                    idCategoria = cuenta.idCategoria
            }
            );

            return resultadoQuery;
        }

        public async Task<Guid> EditarCuenta(Guid id, CuentaRequest cuenta)
        {
            await VerificarExistenciaCuenta(id);

            string sqlQuery = @"sp_Cuenta_Editar";

            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
                {
                    idCuenta = id,
                    nombre = cuenta.Nombre,
                    descripcion = cuenta.Descripcion,
                    permitirSalarioNegativo = cuenta.PermitirSalarioNegativo,
                    fechaUltimaModificacion = DateTime.Now,
                    estado = cuenta.Estado,
                    idCategoria = cuenta.idCategoria
                }
            );

            return resultadoQuery;
        }

        public async Task<Guid> EliminarCuenta(Guid id)
        {
            await VerificarExistenciaCuenta(id);

            string sqlQuery = @"sp_Cuenta_Eliminar";

            var resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new { id });

            return resultadoQuery;
        }

        public async Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas()
        {
            string sqlQuery = @"sp_Cuenta_ObtenerTodos";

            var resultadoQuery = await _sqlConnection.QueryAsync<CuentaResponse>(sqlQuery);

            return resultadoQuery;
        }

        public async Task<CuentaResponse> ObtenerCuentaPorId(Guid id)
        {
            string sqlQuery = @"sp_Cuenta_ObtenerPorId";

            var resultadoQuery = await _sqlConnection.QueryAsync<CuentaResponse>(sqlQuery, new { id });

            return resultadoQuery.FirstOrDefault();
        }

        public async Task<IEnumerable<CuentaResponse>> ObtenerentasPorUsuario(Guid idUsuario)
        {
            string sqlQuery = "sp_Cuenta_ObtenerPorUsuario";

            return await _sqlConnection.QueryAsync<CuentaResponse>(sqlQuery, new { idUsuario });
        }

        private async Task VerificarExistenciaCuenta(Guid id)
        {
            var cuentaCheck = await ObtenerCuentaPorId(id);

            if (cuentaCheck == null)
            {
                throw new Exception("Esta cuenta no existe");
            }
        }
    }
}
