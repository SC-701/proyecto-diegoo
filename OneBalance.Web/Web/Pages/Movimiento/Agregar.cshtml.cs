using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Movimiento
{
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public MovimientoRequest Movimiento { get; set; } = new MovimientoRequest();
        public SelectList Cuentas { get; set; } = default!;
        public SelectList Categorias { get; set; } = default!;
        public SelectList TiposPago { get; set; } = default!;


        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            await CargarDatosAsync();

            Movimiento.Fecha = DateTime.Now;
            Movimiento.Estado = true;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await CargarDatosAsync();
                return Page();
            }

            Movimiento.idMovimiento = Guid.NewGuid();

            try
            {                
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarMovimiento");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, Movimiento);
                response.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Movimiento agregado exitosamente.";
                return RedirectToPage("./Index", new { id = Movimiento.IdCuenta });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar el movimiento: " + ex.Message);
                await CargarDatosAsync();
                return Page();
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                await CargarCuentasAsync();
                await CargarCategoriasAsync();
                await CargarTiposPagoAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los datos: " + ex.Message);
            }
        }

        private async Task CargarCuentasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentasPorUsuario");
            //string usuarioId = "5B435D27-6C13-489A-90A7-675C9FF6C622";
            string usuarioId = "3A9C3399-15F3-4123-BEB8-96CB45AEB18D";

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, usuarioId));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var cuentas = JsonSerializer.Deserialize<IList<CuentaResponse>>(resultado, options) ?? new List<CuentaResponse>();

            Cuentas = new SelectList(cuentas.Where(c => c.Estado), "idCuenta", "Nombre");
        }

        private async Task CargarCategoriasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var categorias = JsonSerializer.Deserialize<IList<CategoriaResponse>>(resultado, options) ?? new List<CategoriaResponse>();

            Categorias = new SelectList(categorias.Where(c => c.Estado), "idCategoria", "Nombre");
        }

        private async Task CargarTiposPagoAsync()
        {
            // CORREGIDO: Cambiar de "ObtenerTipoPago" a "ObtenerTiposPago" o corregir en appsettings.json
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoPago");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var tiposPago = JsonSerializer.Deserialize<IList<TipoPagoResponse>>(resultado, options) ?? new List<TipoPagoResponse>();

            TiposPago = new SelectList(tiposPago.Where(t => t.activestado), "idTipoPago", "nombre");
        }
    }
}