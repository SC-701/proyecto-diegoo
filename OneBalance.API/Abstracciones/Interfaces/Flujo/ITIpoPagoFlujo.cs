using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ITIpoPagoFlujo
    {
        Task<IEnumerable<TipoPagoResponse>> ObtenerTodosLosTiposPago();
        Task<TipoPagoResponse> ObtenerTipoPagoPorId(Guid id);
        Task<Guid> CrearTipoPago(TipoPagoRequest tipoPago);
        Task<Guid> EditarTipoPago(Guid id, TipoPagoRequest tipoPago);
        Task<Guid> EliminarTipoPago(Guid id);
    }
}
