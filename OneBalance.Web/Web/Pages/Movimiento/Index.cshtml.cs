using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Movimiento
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IConfiguracion _configuracion;
        public IList<MovimientoResponse> movimientos { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, IConfiguracion configuracion)
        {
            _logger = logger;
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de la cuenta no puede estar vacío.");
                return;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "Ultimos5Movimientos");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            movimientos = JsonSerializer.Deserialize<IList<MovimientoResponse>>(resultado, options);
        }
    }
}