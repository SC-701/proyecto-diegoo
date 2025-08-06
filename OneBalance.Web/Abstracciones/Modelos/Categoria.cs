using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriaBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }

    public class CategoriaRequest : CategoriaBase
    {
        public Guid idCategoria { get; set; }
    }

    public class CategoriaResponse : CategoriaBase
    {
        public Guid idCategoria { get; set; }
    }

}
