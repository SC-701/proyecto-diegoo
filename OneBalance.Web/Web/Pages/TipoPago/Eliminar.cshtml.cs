using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoPago
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public TipoPagoResponse TipoPago { get; set; }

        public EliminarModel(IConfiguracion configuracion)
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

            // Usa el endpoint correcto para obtener el detalle
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoPagoPorId");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            TipoPago = JsonSerializer.Deserialize<TipoPagoResponse>(resultado, options);
        }

        public async Task<ActionResult> OnPost()
        {
            if (TipoPago.idTipoPago == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de no puede estar vacío.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarTipoPago");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, TipoPago.idTipoPago));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
