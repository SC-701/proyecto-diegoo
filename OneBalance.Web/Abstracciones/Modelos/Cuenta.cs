using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CuentaBase
    {
        [Required(ErrorMessage = "Debe indicar el nombre de la cuenta")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicar la descripción de la cuenta")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe indicar el tipo de cuenta")]
        public Guid idCategoria { get; set; }

        [Required(ErrorMessage = "Debe indicar si quiere permitir saldo negativo o no")]
        public bool PermitirSalarioNegativo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Estado { get; set; }
    }

    public class CuentaRequest : CuentaBase
    {
        public Guid idCuenta { get; set; }
        public Guid IdUsuario { get; set; }
    }

    public class CuentaResponse : CuentaBase
    {
        public Guid idCuenta { get; set; }
        public string Usuario { get; set; }
    }
}
