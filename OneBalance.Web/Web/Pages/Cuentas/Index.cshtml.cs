using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Cuentas
{
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<CuentaResponse> cuentas { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            await CargarCuentas();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCuenta");

                using (HttpClient client = new HttpClient())
                {
                    string url = string.Format(endpoint, id);
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Cuenta eliminada exitosamente.";
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al eliminar la cuenta: {errorContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la cuenta: {ex.Message}";
            }

            return RedirectToPage();
        }

        private async Task CargarCuentas()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentasPorUsuario");

                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, "3A9C3399-15F3-4123-BEB8-96CB45AEB18D"));
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    string resultado = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    cuentas = JsonSerializer.Deserialize<IList<CuentaResponse>>(resultado, options) ?? new List<CuentaResponse>();
                }
            }
            catch (Exception ex)
            {
                cuentas = new List<CuentaResponse>();
                TempData["ErrorMessage"] = $"Error al cargar las cuentas: {ex.Message}";
            }
        }
    }
}
