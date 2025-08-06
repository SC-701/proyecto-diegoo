using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private IConfiguration _configuracion;

        public Configuracion(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public string ObtenerMetodo(string seccion, string nombrePropiedad)
        {
            string urlBase = _configuracion.GetSection(seccion).Get<ApiEndpoint>().UrlBase;

            var metodo = _configuracion.GetSection(seccion).Get<ApiEndpoint>().Metodos
                .Where(m => m.Nombre == nombrePropiedad).FirstOrDefault().Valor;

            return $"{urlBase}/{metodo}";
        }
    }
}
