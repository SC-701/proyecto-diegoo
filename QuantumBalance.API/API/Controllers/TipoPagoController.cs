using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagoController : Controller, ITipoPagoController
    {
        private readonly ITIpoPagoFlujo _tipoPagoFlujo;
        private readonly ILogger<TipoPagoController> _logger;

        public TipoPagoController(ITIpoPagoFlujo tipoPagoFlujo, ILogger<TipoPagoController> logger)
        {
            _tipoPagoFlujo = tipoPagoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTipoPago([FromBody] TipoPagoRequest tipoPago)
        {
            Guid nuevoTipo = await _tipoPagoFlujo.CrearTipoPago(tipoPago);

            if (nuevoTipo == Guid.Empty)
            {
                _logger.LogError("No se creo el tipo de pago. Entrada {@TipoPago}", tipoPago);
                return BadRequest("No se pudo crear el tipo de pago.");
            }

            return CreatedAtAction(nameof(ObtenerTipoPagoPorId), new { id = nuevoTipo }, null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarTipoPago([FromQuery] Guid id, [FromBody] TipoPagoRequest tipoPago)
        {
            if(id == Guid.Empty || tipoPago == null)
            {
                _logger.LogError("ID de tipo de pago inválido o tipo de pago nulo.");
                return BadRequest("ID de tipo de pago inválido o tipo de pago nulo.");
            }

            Guid resultado = await _tipoPagoFlujo.EditarTipoPago(id, tipoPago);

            if(resultado == Guid.Empty)
            {
                _logger.LogError("Error al editar el tipo de pago.");
                return BadRequest("Error al editar el tipo de pago.");
            }

            return Ok(resultado);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarTipoPago([FromQuery] Guid id)
        {
            Guid resultado = await _tipoPagoFlujo.EliminarTipoPago(id);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar el tipo de pago.");
                return BadRequest("Error al eliminar el tipo de pago.");
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTipoPagoPorId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("ID de tipo de pago inválido.");
                return BadRequest("ID de tipo de pago inválido.");
            }

            TipoPagoResponse resultado = await _tipoPagoFlujo.ObtenerTipoPagoPorId(id);

            if (resultado == null)
            {
                _logger.LogError("No se encontró el tipo de pago con ID {Id}.", id);
                return NotFound($"No se encontró el tipo de pago con ID {id}.");
            }

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosTiposPago()
        {
            IEnumerable<TipoPagoResponse> resultado = await _tipoPagoFlujo.ObtenerTodosLosTiposPago();

            if(resultado == null || !resultado.Any())
            {
                _logger.LogError("No se encontraron tipos de pago.");
                return NotFound("No se encontraron tipos de pago.");
            }

            return Ok(resultado);
        }
    }
}
