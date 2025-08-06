using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class CuentaFlujo : ICuentaFlujo
    {
        private readonly ICuentaDA _cuentaDA;

        public CuentaFlujo(ICuentaDA cuentaDA)
        {
            _cuentaDA = cuentaDA;
        }

        public async Task<Guid> CrearCuenta(CuentaRequest cuenta)
        {
            return await _cuentaDA.CrearCuenta(cuenta);
        }

        public async Task<Guid> EditarCuenta(Guid id, CuentaRequest cuenta)
        {
            return await _cuentaDA.EditarCuenta(id, cuenta);
        }

        public async Task<Guid> EliminarCuenta(Guid id)
        {
            return await _cuentaDA.EliminarCuenta(id);
        }

        public async Task<CuentaResponse> ObtenerCuentaPorId(Guid id)
        {
            return await _cuentaDA.ObtenerCuentaPorId(id);
        }

        public async Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas()
        {
            return await _cuentaDA.ObtenerTodasLasCuentas();
        }
    }
}
