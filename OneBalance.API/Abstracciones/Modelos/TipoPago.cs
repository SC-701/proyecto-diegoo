using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class TipoPagoBase
    {
        [Required(ErrorMessage = "Debe indicar el nombre del tipo de pago")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicar la descripción del tipo de pago")]
        public string Descripcion { get; set; }
        public bool activestado { get; set; }
    }

    public class TipoPagoRequest : TipoPagoBase
    {
        public Guid idTipoPago { get; set; }
    }

    public class TipoPagoResponse : TipoPagoBase
    {
        public Guid idTipoPago { get; set; }
    }
}
