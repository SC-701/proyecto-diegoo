using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.TipoPago
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public TipoPagoResponse TipoPago { get; set; }

        public EditarModel(IConfiguracion configuracion)
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

        public async Task<ActionResult> OnPost()
        {
            if (TipoPago.idTipoPago == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID del tipo de pago no puede estar vacío.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarTipoPago");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync<TipoPagoRequest>(string.Format(endpoint, TipoPago.idTipoPago), new TipoPagoRequest
            {
                idTipoPago = TipoPago.idTipoPago,
                nombre = TipoPago.nombre,
                descripcion = TipoPago.descripcion
            });
            response.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}