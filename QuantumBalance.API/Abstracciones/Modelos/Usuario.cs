using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class UsuarioBase
    {
        [Required(ErrorMessage = "Debe indicar el nombre del usuario")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicar el primer apellido del usuario")]
        [StringLength(100)]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "Debe indicar el segundo apellido del usuario")]
        [StringLength(100)]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "Debe indicar el correo del usuario")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe indicar la contraseña del usuario")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe indicar la moneda principal del usuario")]
        public string MonedaPrincipal { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public bool Estado { get; set; }
    }

    public class UsuarioRequest : UsuarioBase
    {
        public Guid idUsuario { get; set; }
    }

    public class UsuarioResponse : UsuarioBase
    {
        public Guid idUsuario { get; set; }
    }
}
