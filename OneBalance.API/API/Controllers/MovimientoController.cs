using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : Controller, IMovimientoController
    {
        private readonly ILogger<MovimientoController> _logger;
        private readonly IMovimientoFlujo _movimientoFlujo;

        public MovimientoController(IMovimientoFlujo movimientoFlujo, ILogger<MovimientoController> logger)
        {
            _movimientoFlujo = movimientoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoRequest movimiento)
        {
            Guid nuevoMovimiento = await _movimientoFlujo.CrearMovimiento(movimiento);

            if (nuevoMovimiento == Guid.Empty)
            {
                _logger.LogError("No se pudo crear el movimiento. Entrada: {@Movimiento}", movimiento);
                return BadRequest("No se pudo crear el movimiento.");
            }

            return CreatedAtAction(nameof(ObtenerMovimientoPorId), new { id = nuevoMovimiento }, null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarMovimiento([FromQuery] Guid id, [FromBody] MovimientoRequest movimiento)
        {
            if (id == Guid.Empty || movimiento == null)
            {
                _logger.LogError("ID de movimiento inválido o movimiento nulo.");
                return BadRequest("ID de movimiento inválido o movimiento nulo.");
            } 

            Guid movimientoEditado = await _movimientoFlujo.EditarMovimiento(id, movimiento);

            if (movimientoEditado == Guid.Empty)
            {
                _logger.LogError("Error al editar el movimiento.");
                return BadRequest("Error al editar el movimiento.");
            }

            return Ok(movimientoEditado);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarMovimiento([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("ID de movimiento inválido.");
                return BadRequest("ID de movimiento inválido.");
            }


            Guid resultado = await _movimientoFlujo.EliminarMovimiento(id);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar el movimiento.");
                return BadRequest("Error al eliminar el movimiento.");
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerMovimientoPorId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("ID de movimiento inválido.");
                return BadRequest("ID de movimiento inválido.");
            }

            MovimientoResponse movimiento = await _movimientoFlujo.ObtenerMovimientoPorId(id);

            if (movimiento == null)
            {
                _logger.LogError("No se encontró el movimiento con ID: {Id}", id);
                return NotFound($"No se encontró el movimiento con ID: {id}");
            }

            return Ok(movimiento);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosMovimientos()
        {
            IEnumerable<MovimientoResponse> movimientos = await _movimientoFlujo.ObtenerTodosLosMovimientos();

            if (movimientos == null || !movimientos.Any())
            {
                _logger.LogError("No se encontraron movimientos.");
                return NotFound("No se encontraron movimientos.");
            }

            return Ok(movimientos);
        }
    }
}
