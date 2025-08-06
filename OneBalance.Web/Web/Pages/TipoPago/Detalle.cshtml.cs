using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoPago
{
    public class DetalleModel : PageModel
    {
        private IConfiguracion _configuracion;
        public TipoPagoResponse TipoPago { get; set; }

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de no puede estar vacío.");
                return;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoPagoPorId");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            string resultado = response.Content.ReadAsStringAsync().Result;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            TipoPago = JsonSerializer.Deserialize<TipoPagoResponse>(resultado, options);
        }
    }
}