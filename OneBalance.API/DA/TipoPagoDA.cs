using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class TipoPagoDA : ITipoPagoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public TipoPagoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerConexion();
        }

        public async Task<Guid> CrearTipoPago(TipoPagoRequest tipoPago)
        {
            string sqlQuery = @"sp_TipoPago_Crear";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idTipoPago = Guid.NewGuid(),
                nombre = tipoPago.Nombre,
                descripcion = tipoPago.Descripcion,
                activestado = true
            });

            return resultadoQuery;
        }

        public async Task<Guid> EditarTipoPago(Guid id, TipoPagoRequest tipoPago)
        {
            await VerificarTipoPago(id);

            string sqlQuery = @"sp_TipoPago_Editar";

            Guid resultadoQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new
            {
                idTipoPago = id,
                nombre = tipoPago.Nombre,
                descripcion = tipoPago.Descripcion,
                activestado = tipoPago.activestado
            });

            return resultadoQuery;
        }

        public async Task<Guid> EliminarTipoPago(Guid id)
        {
            await VerificarTipoPago(id);

            string sqlQuery = @"sp_TipoPago_Eliminar";

            return await _sqlConnection.ExecuteScalarAsync<Guid>(sqlQuery, new { idTipoPago = id });
        }

        public async Task<TipoPagoResponse> ObtenerTipoPagoPorId(Guid id)
        {
            string sqlQuery = @"sp_TipoPago_ObtenerPorId";

            return await _sqlConnection.QuerySingleOrDefaultAsync<TipoPagoResponse>(sqlQuery, new { idTipoPago = id });
        }

        public Task<IEnumerable<TipoPagoResponse>> ObtenerTodosLosTiposPago()
        {
            return _sqlConnection.QueryAsync<TipoPagoResponse>(@"sp_TipoPago_ObtenerTodos");
        }

        private async Task VerificarTipoPago(Guid id)
        {
            var tipoPagoCheck = await ObtenerTipoPagoPorId(id);

            if (tipoPagoCheck == null)
            {
                throw new Exception("Esta cuenta no existe");
            }
        }
    }
}