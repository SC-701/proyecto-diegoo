using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario)
        {
            return await _usuarioDA.EditarUsuario(id, usuario);
        }

        public async Task<Guid> EliminarUsuario(Guid id)
        {
            return await _usuarioDA.EliminarUsuario(id);
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(id);
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios()
        {
            return await _usuarioDA.ObtenerTodosLosUsuarios();
        }
    }
}
