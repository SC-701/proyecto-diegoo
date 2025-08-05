using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios();
        Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id);
        Task<Guid> CrearUsuario(UsuarioRequest usuario);
        Task<Guid> EditarUsuario(Guid id, UsuarioRequest usuario);
        Task<Guid> EliminarUsuario(Guid id);
    }
}
