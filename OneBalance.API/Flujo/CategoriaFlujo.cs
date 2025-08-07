using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private readonly ICategoriaDA _categoriaDA;

        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _categoriaDA = categoriaDA;
        }

        public async Task<Guid> CrearCategoria(CategoriaRequest categoria)
        {
            return await _categoriaDA.CrearCategoria(categoria);
        }

        public async Task<Guid> EditarCategoria(Guid id, CategoriaRequest categoria)
        {
            return await _categoriaDA.EditarCategoria(id, categoria);
        }

        public async Task<Guid> EliminarCategoria(Guid id)
        {
            return await _categoriaDA.EliminarCategoria(id);
        }

        public async Task<CategoriaResponse> ObtenerCategoriaPorId(Guid id)
        {
            return await _categoriaDA.ObtenerCategoriaPorId(id);
        }

        public async Task<IEnumerable<CategoriaResponse>> ObtenerTodasLasCategorias()
        {
            return await _categoriaDA.ObtenerTodasLasCategorias();
        }
    }
}
