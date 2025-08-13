using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class MovimientoBase
    {
        [Required(ErrorMessage = "Debe indicar la descripción del movimiento")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe indicar el monto inicial de la cuenta")]
        public decimal MontoOriginal { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }

    public class MovimientoRequest : MovimientoBase
    {
        public Guid idMovimiento { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdTipoPago { get; set; }
    }

    public class MovimientoResponse : MovimientoRequest { }
}
