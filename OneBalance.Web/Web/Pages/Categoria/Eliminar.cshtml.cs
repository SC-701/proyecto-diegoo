using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Categoria
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public CategoriaResponse Categoria { get; set; }

        public EliminarModel(IConfiguracion configuracion)
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

            Categoria = JsonSerializer.Deserialize<CategoriaResponse>(resultado, options);
        }

        public async Task<ActionResult> OnPost()
        {
            if (Categoria.idCategoria == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de la categoría no puede estar vacío.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCategoria");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, Categoria.idCategoria));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
