using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICategoriaController
    {
        Task<IActionResult> ObtenerTodasLasCategorias();
        Task<IActionResult> ObtenerCategoriaPorId(Guid id);
        Task<IActionResult> CrearCategoria(CategoriaRequest categoria);
        Task<IActionResult> EditarCategoria(Guid id, CategoriaRequest categoria);
        Task<IActionResult> EliminarCategoria(Guid id);
    }
}
