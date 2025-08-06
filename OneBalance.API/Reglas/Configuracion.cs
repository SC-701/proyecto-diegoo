using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombrePropiedad)
        {
            var endpoint = _configuration.GetSection(seccion).Get<ApiEndpoint>();
            var metodo = endpoint?.Metodos?.FirstOrDefault(m => m.Nombre == nombrePropiedad)?.Valor;

            return $"{endpoint?.UrlBase}/{metodo}";
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value;
        }
    }
}
