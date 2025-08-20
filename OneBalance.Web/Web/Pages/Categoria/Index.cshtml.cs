using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Categoria
{
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<CategoriaResponse> categorias { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            categorias = JsonSerializer.Deserialize<List<CategoriaResponse>>(resultado, options);
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCategoria");

                using (HttpClient client = new HttpClient())
                {
                    string url = string.Format(endpoint, id);
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Categoría eliminada exitosamente.";
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al eliminar la categoría: {errorContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la categoría: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
