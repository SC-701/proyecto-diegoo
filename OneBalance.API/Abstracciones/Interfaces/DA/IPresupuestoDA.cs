using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IPresupuestoDA
    {
        Task<IEnumerable<PresupuestoResponse>> ObtenerTodosLosPresupuestos();
        Task<PresupuestoResponse> ObtenerPresupuestoPorId(Guid id);
        Task<Guid> CrearPresupuesto(PresupuestoRequest presupuesto);
        Task<Guid> EditarPresupuesto(Guid id, PresupuestoRequest presupuesto);
        Task<Guid> EliminarPresupuesto(Guid id);
    }
}
