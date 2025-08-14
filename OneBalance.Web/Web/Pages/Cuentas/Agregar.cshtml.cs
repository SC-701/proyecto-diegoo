using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Cuenta
{
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaRequest Cuenta { get; set; } = new CuentaRequest();
        public SelectList Categorias { get; set; } = default!;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            await CargarDatos();

            Cuenta.FechaCreacion = DateTime.Now;
            Cuenta.FechaUltimaModificacion = DateTime.Now;
            Cuenta.Estado = true;
            Cuenta.PermitirSalarioNegativo = false;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await CargarDatos();
                return Page();
            }

            Cuenta.idCuenta = Guid.NewGuid();
            //Cuenta.IdUsuario = Guid.Parse("5B435D27-6C13-489A-90A7-675C9FF6C622"); 
            Cuenta.IdUsuario = Guid.Parse("3A9C3399-15F3-4123-BEB8-96CB45AEB18D");
            Cuenta.FechaCreacion = DateTime.Now;
            Cuenta.FechaUltimaModificacion = DateTime.Now;

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCuenta");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, Cuenta);
                response.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Cuenta creada exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al crear la cuenta: " + ex.Message);
                await CargarDatos();
                return Page();
            }
        }

        private async Task CargarDatos()
        {
            try
            {
                await CargarCategorias();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los datos: " + ex.Message);
            }
        }

        private async Task CargarCategorias()
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
    }
}