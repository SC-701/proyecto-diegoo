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
            string endopint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentasPorUsuario");

            HttpClient client = new HttpClient();
            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endopint, "5B435D27-6C13-489A-90A7-675C9FF6C622"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endopint, "3A9C3399-15F3-4123-BEB8-96CB45AEB18D"));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            cuentas = JsonSerializer.Deserialize<IList<CuentaResponse>>(resultado, options);
        }
    }
}