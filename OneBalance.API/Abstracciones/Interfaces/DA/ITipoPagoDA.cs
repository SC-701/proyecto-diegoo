using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ITipoPagoDA
    {
        Task<IEnumerable<TipoPagoResponse>> ObtenerTodosLosTiposPago();
        Task<TipoPagoResponse> ObtenerTipoPagoPorId(Guid id);
        Task<Guid> CrearTipoPago(TipoPagoRequest tipoPago);
        Task<Guid> EditarTipoPago(Guid id, TipoPagoRequest tipoPago);
        Task<Guid> EliminarTipoPago(Guid id);
    }
}
