using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMovimientoFlujo
    {
        Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos();
        Task<MovimientoResponse> ObtenerMovimientoPorId(Guid id);
        Task<Guid> CrearMovimiento(MovimientoRequest movimiento);
        Task<Guid> EditarMovimiento(Guid id, MovimientoRequest movimiento);
        Task<Guid> EliminarMovimiento(Guid id);
    }
}
