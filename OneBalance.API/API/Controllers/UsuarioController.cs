using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _usuarioFlujo;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioFlujo usuarioFlujo, ILogger<UsuarioController> logger)
        {
            _usuarioFlujo = usuarioFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest usuario)
        {
            Guid resultado = await _usuarioFlujo.CrearUsuario(usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear el usuario. Entrada: {@Usuario}", usuario);
                return BadRequest("No se pudo crear el usuario.");
            }
            return CreatedAtAction(nameof(ObtenerUsuarioPorId),
                new { id = resultado },
                null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario([FromQuery] Guid id, [FromBody] UsuarioRequest usuario)
        {
            if (id == Guid.Empty || usuario == null)
            {
                _logger.LogError("ID de usuario inválido o usuario nulo.");
                return BadRequest("ID de usuario inválido o usuario nulo.");
            }
            Guid resultado = await _usuarioFlujo.EditarUsuario(id, usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al actualizar el usuario.");
                return BadRequest("Error al actualizar el usuario.");
            }
            return Ok(resultado);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario([FromQuery] Guid id)
        {
            Guid resultado = await _usuarioFlujo.EliminarUsuario(id);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar el usuario.");
                return BadRequest("No se pudo eliminar el usuario.");
            }
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("ID de usuario inválido.");
                return BadRequest("ID de usuario inválido.");
            }
            UsuarioResponse usuario = await _usuarioFlujo.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado. ID: {Id}", id);
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet()]
        public async Task<IActionResult> ObtenerTodosLosUsuarios()
        {
            IEnumerable<UsuarioResponse> usuarios = await _usuarioFlujo.ObtenerTodosLosUsuarios();

            if (usuarios == null || !usuarios.Any())
            {
                _logger.LogWarning("No se encontraron usuarios.");
                return NotFound("No se encontraron usuarios.");
            }
            return Ok(usuarios);
        }
    }
}
