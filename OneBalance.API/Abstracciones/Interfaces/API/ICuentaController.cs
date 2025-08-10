using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICuentaController
    {
        Task<IActionResult> ObtenerTodasLasCuentas();
        Task<IActionResult> ObtenerCuentaPorId(Guid id);
        Task<IActionResult> CrearCuenta(CuentaRequest cuenta);
        Task<IActionResult> EditarCuenta(Guid id, CuentaRequest cuenta);
        Task<IActionResult> EliminarCuenta(Guid id);
        Task<IActionResult> ObtenerentasPorUsuario(Guid idUsuario);
    }
}
