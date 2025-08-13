using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MovimientoDA : IMovimientoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public MovimientoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearMovimiento(MovimientoRequest movimiento)
        {
            string sqlQuery = @"sp_Movimiento_Crear";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idMovimiento = Guid.NewGuid(),
                idCuenta = movimiento.IdCuenta,
                idCategoria = movimiento.IdCategoria,
                idTipoPago = movimiento.IdTipoPago,
                descripcion = movimiento.Descripcion,
                montoOriginal = movimiento.MontoOriginal,
                fecha = movimiento.Fecha,
                estado = movimiento.Estado
            });

            return resultadoQuery;
        }

        public async Task<Guid> EditarMovimiento(Guid id, MovimientoRequest movimiento)
        {
            await VerificarMovimiento(id);

            string sqlQuery = @"sp_Movimiento_Editar";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idMovimiento = id,                
                idCategoria = movimiento.IdCategoria,
                idTipoPago = movimiento.IdTipoPago,
                descripcion = movimiento.Descripcion,
                montoOriginal = movimiento.MontoOriginal,               
                estado = movimiento.Estado
            });

            return resultadoQuery;
        }

        public async Task<Guid> EliminarMovimiento(Guid id)
        {
            await VerificarMovimiento(id);

            string sqlQuery = @"sp_Movimiento_Eliminar";

            return await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new { idMovimiento = id });
        }

        public async Task<MovimientoResponse> ObtenerMovimientoPorId(Guid id)
        {
            string sqlQuery = "sp_Movimiento_ObtenerPorId";

            return await _sqlConnection.QueryFirstOrDefaultAsync<MovimientoResponse>(sqlQuery, new { idMovimiento = id });
        }

        public Task<IEnumerable<MovimientoResponse>> ObtenerMovimientosPorCuenta(Guid idCuenta)
        {
            string sqlQuery = "sp_Movimiento_ObtenerUltimos5PorCuenta";

            return _sqlConnection.QueryAsync<MovimientoResponse>(sqlQuery, new { idCuenta });
        }

        public Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos()
        {
            string sqlQuery = "sp_Movimiento_ObtenerTodos";

            return _sqlConnection.QueryAsync<MovimientoResponse>(sqlQuery);
        }

        private async Task VerificarMovimiento(Guid id)
        {
            MovimientoResponse movimientoCheck = await ObtenerMovimientoPorId(id);

            if (movimientoCheck == null)
            {
                throw new Exception("El movimiento no existe");
            }
        }
    }
}
