using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class TipoPagoFlujo : ITIpoPagoFlujo
    {
        private readonly ITipoPagoDA _tipoPagoDA;

        public TipoPagoFlujo(ITipoPagoDA tipoPagoDA)
        {
            _tipoPagoDA = tipoPagoDA;
        }

        public async Task<Guid> CrearTipoPago(TipoPagoRequest tipoPago)
        {
            return await _tipoPagoDA.CrearTipoPago(tipoPago);
        }

        public async Task<Guid> EditarTipoPago(Guid id, TipoPagoRequest tipoPago)
        {
            return await _tipoPagoDA.EditarTipoPago(id, tipoPago);
        }

        public async Task<Guid> EliminarTipoPago(Guid id)
        {
            return await _tipoPagoDA.EliminarTipoPago(id);
        }

        public async Task<TipoPagoResponse> ObtenerTipoPagoPorId(Guid id)
        {
            return await _tipoPagoDA.ObtenerTipoPagoPorId(id);
        }

        public async Task<IEnumerable<TipoPagoResponse>> ObtenerTodosLosTiposPago()
        {
            return await _tipoPagoDA.ObtenerTodosLosTiposPago();
        }
    }
}