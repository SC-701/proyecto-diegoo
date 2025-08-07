using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICategoriaFlujo
    {
        Task<IEnumerable<CategoriaResponse>> ObtenerTodasLasCategorias();
        Task<CategoriaResponse> ObtenerCategoriaPorId(Guid id);
        Task<Guid> CrearCategoria(CategoriaRequest categoria);
        Task<Guid> EditarCategoria(Guid id, CategoriaRequest categoria);
        Task<Guid> EliminarCategoria(Guid id);
    }
}
