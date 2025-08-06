using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class PresupuestoBase
    {
        [Required(ErrorMessage = "Debe indicar el nobre del presupuesto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicar el monto limite del presupuesto")]
        public decimal MontoLimite { get; set; }

        public decimal MontoGastado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }

    public class PresupuestoRequest : PresupuestoBase
    {
        public Guid idPresupuesto { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
    }

    public class PresupuestoResponse : PresupuestoRequest { }
}
