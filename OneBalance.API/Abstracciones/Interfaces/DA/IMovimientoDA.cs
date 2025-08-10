using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IMovimientoDA
    {
        Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos();
        Task<MovimientoResponse> ObtenerMovimientoPorId(Guid id);
        Task<Guid> CrearMovimiento(MovimientoRequest movimiento);
        Task<Guid> EditarMovimiento(Guid id, MovimientoRequest movimiento);
        Task<Guid> EliminarMovimiento(Guid id);
        Task<IEnumerable<MovimientoResponse>> ObtenerMovimientosPorCuenta(Guid idCuenta);
    }
}
