using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller, ICategoriaController
    {
        private readonly ICategoriaFlujo _categoriaFlujo;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaFlujo categoriaFlujo, ILogger<CategoriaController> logger)
        {
            _categoriaFlujo = categoriaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaRequest categoria)
        {
            Guid nuevaCuenta = await _categoriaFlujo.CrearCategoria(categoria);

            if (nuevaCuenta == Guid.Empty)
            {
                _logger.LogError("No se pudo crear la categoría. Entrada: {@Categoria}", categoria);
                return BadRequest("No se pudo crear la categoría.");
            }

            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = nuevaCuenta }, null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarCategoria([FromQuery] Guid id, [FromBody] CategoriaRequest categoria)
        {
            if (id == Guid.Empty || categoria == null)
            {
                _logger.LogError("ID de categoría inválido o categoría nula.");
                return BadRequest("ID de categoría inválido o categoría nula.");
            }

            Guid resultado = await _categoriaFlujo.EditarCategoria(id, categoria);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al editar la categoría.");
                return BadRequest("Error al editar la categoría.");
            }

            return Ok(resultado);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCategoria([FromQuery] Guid id)
        {
            Guid resultado = await _categoriaFlujo.EliminarCategoria(id);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar la categoría con ID {Id}.", id);
                return BadRequest("Error al eliminar la categoría.");
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoriaPorId([FromRoute] Guid id)
        {
            CategoriaResponse categoria = await _categoriaFlujo.ObtenerCategoriaPorId(id);

            if (categoria == null)
            {
                _logger.LogError($"No se encontraron categorías para el ID {id}.");
                return NotFound("Categoría no encontrada.");
            }

            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasCategorias()
        {
            IEnumerable<CategoriaResponse> categorias = await _categoriaFlujo.ObtenerTodasLasCategorias();

            if (categorias == null || !categorias.Any())
            {
                _logger.LogError("No se encontraron categorías.");
                return NotFound("No se encontraron categorías.");
            }

            return Ok(categorias);
        }
    }
}
