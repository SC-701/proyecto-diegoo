using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CuentaCategoriaRequest
    {
        [Required(ErrorMessage = "La propiedad idCuenta es requerida")]
        public Guid IdCuenta { get; set; }

        [Required(ErrorMessage = "La propiedad idCategoria es requerida")]
        public Guid IdCategoria { get; set; }
    }

    public class CuentaCategoria : CuentaCategoriaRequest { }
}
