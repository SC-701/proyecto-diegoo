using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICuentaDA
    {
        Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas();
        Task<CuentaResponse> ObtenerCuentaPorId(Guid id);
        Task<Guid> CrearCuenta(CuentaRequest cuenta);
        Task<Guid> EditarCuenta(Guid id, CuentaRequest cuenta);
        Task<Guid> EliminarCuenta(Guid id);
        Task<IEnumerable<CuentaResponse>> ObtenerentasPorUsuario(Guid idUsuario);
    }
}
