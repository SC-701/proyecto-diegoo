using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IPresupuestoController
    {
        Task<IActionResult> ObtenerTodosLosPresupuestos();
        Task<IActionResult> ObtenerPresupuestoPorId(Guid id);
        Task<IActionResult> CrearPresupuesto(PresupuestoRequest presupuesto);
        Task<IActionResult> EditarPresupuesto(Guid id, PresupuestoRequest presupuesto);
        Task<IActionResult> EliminarPresupuesto(Guid id);
    }
}
