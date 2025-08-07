using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoPago
{
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<TipoPagoResponse> tiposPago { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoPago");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            tiposPago = JsonSerializer.Deserialize<List<TipoPagoResponse>>(resultado, options);
        }
    }
}