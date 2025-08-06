using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Categoria
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public CategoriaResponse categoria { get; set; }

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de la categoría no puede estar vacío.");
                return;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriaPorId");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            categoria = JsonSerializer.Deserialize<CategoriaResponse>(resultado, options);
        }
    }
}
