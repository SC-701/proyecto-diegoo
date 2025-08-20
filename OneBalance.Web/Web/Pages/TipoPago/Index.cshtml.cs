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

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    TempData["ErrorMessage"] = "ID de tipo de pago inválido.";
                    return RedirectToPage();
                }

                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarTipoPago");

                using (HttpClient client = new HttpClient())
                {
                    string url = string.Format(endpoint, id);
                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Tipo de pago eliminado exitosamente.";
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        var errorMessage = $"Error {response.StatusCode}";

                        if (!string.IsNullOrEmpty(errorContent))
                        {
                            errorMessage += $": {errorContent}";
                        }

                        Console.WriteLine($"Error eliminando tipo de pago {id}: {response.StatusCode} - {errorContent}");
                        TempData["ErrorMessage"] = errorMessage;
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                TempData["ErrorMessage"] = $"Error de conexión al eliminar el tipo de pago: {httpEx.Message}";
                Console.WriteLine($"HttpRequestException: {httpEx}");
            }
            catch (TaskCanceledException timeoutEx)
            {
                TempData["ErrorMessage"] = "La operación de eliminación excedió el tiempo límite.";
                Console.WriteLine($"TaskCanceledException: {timeoutEx}");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al eliminar el tipo de pago: {ex.Message}";
                Console.WriteLine($"Exception: {ex}");
            }

            return RedirectToPage();
        }
    }
}