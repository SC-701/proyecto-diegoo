using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICuentaCategoriaFlujo
    {
        Task<IEnumerable<CuentaCategoria>> ObtenerTodasLasCuentasCategorias();
        Task<CuentaCategoria> ObtenerCuentaCategoriaPorId(Guid id);
        Task<Guid> CrearCuentaCategoria(CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EditarCuentaCategoria(Guid id, CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EliminarCuentaCategoria(Guid id);
    }
}
