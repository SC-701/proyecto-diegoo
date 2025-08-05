using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ITipoPagoController
    {
        Task<IActionResult> ObtenerTodosLosTiposPago();
        Task<IActionResult> ObtenerTipoPagoPorId(Guid id);
        Task<IActionResult> CrearTipoPago(TipoPagoRequest tipoPago);
        Task<IActionResult> EditarTipoPago(Guid id, TipoPagoRequest tipoPago);
        Task<IActionResult> EliminarTipoPago(Guid id);
    }
}
