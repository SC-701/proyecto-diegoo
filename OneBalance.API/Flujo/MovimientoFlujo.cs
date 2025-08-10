using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class MovimientoFlujo : IMovimientoFlujo
    {
        private readonly IMovimientoDA _movimientoDa;

        public MovimientoFlujo(IMovimientoDA movimientoDa)
        {
            _movimientoDa = movimientoDa;
        }

        public async Task<Guid> CrearMovimiento(MovimientoRequest movimiento)
        {
            return await _movimientoDa.CrearMovimiento(movimiento);
        }

        public async Task<Guid> EditarMovimiento(Guid id, MovimientoRequest movimiento)
        {
            return await _movimientoDa.EditarMovimiento(id, movimiento);
        }

        public async Task<Guid> EliminarMovimiento(Guid id)
        {
            return await _movimientoDa.EliminarMovimiento(id);
        }

        public async Task<MovimientoResponse> ObtenerMovimientoPorId(Guid id)
        {
            return await _movimientoDa.ObtenerMovimientoPorId(id);
        }

        public async Task<IEnumerable<MovimientoResponse>> ObtenerMovimientosPorCuenta(Guid idCuenta)
        {
            return await _movimientoDa.ObtenerMovimientosPorCuenta(idCuenta);
        }

        public async Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos()
        {
            return await _movimientoDa.ObtenerTodosLosMovimientos();
        }
    }
}