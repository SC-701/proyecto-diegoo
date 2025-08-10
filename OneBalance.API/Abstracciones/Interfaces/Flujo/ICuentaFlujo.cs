using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICuentaFlujo
    {
        Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas();
        Task<CuentaResponse> ObtenerCuentaPorId(Guid id);
        Task<Guid> CrearCuenta(CuentaRequest cuenta);
        Task<Guid> EditarCuenta(Guid id, CuentaRequest cuenta);
        Task<Guid> EliminarCuenta(Guid id);
        Task<IEnumerable<CuentaResponse>> ObtenerentasPorUsuario(Guid idUsuario);
    }
}
