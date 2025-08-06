using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICuentaCategoriaDA
    {
        Task<IEnumerable<CuentaCategoria>> ObtenerTodasLasCuentasCategorias();
        Task<CuentaCategoria> ObtenerCuentaCategoriaPorId(Guid id);
        Task<Guid> CrearCuentaCategoria(CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EditarCuentaCategoria(Guid id, CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EliminarCuentaCategoria(Guid id);
    }
}
