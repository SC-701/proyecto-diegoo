using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class EditarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaRequest Cuenta { get; set; } = new CuentaRequest();
        public SelectList Categorias { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {                
                await CargarCategorias();
             
                await CargarCuenta(id.Value);

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar la cuenta: " + ex.Message);
                
                try
                {
                    if (Categorias == null)
                    {
                        await CargarCategorias();
                    }
                }
                catch
                {
                    Categorias = new SelectList(new List<CategoriaResponse>(), "idCategoria", "Nombre");
                }

                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await CargarDatos();
                return Page();
            }
            
            Cuenta.FechaUltimaModificacion = DateTime.Now;

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
                string url = string.Format(endpoint, Cuenta.idCuenta);

                HttpClient client = new HttpClient();
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                HttpResponseMessage response = await client.PutAsJsonAsync(url, Cuenta, options);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error {response.StatusCode}: {errorContent}");
                }

                TempData["SuccessMessage"] = "Cuenta actualizada exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la cuenta: " + ex.Message);
                await CargarDatos();
                return Page();
            }
        }

        private async Task CargarCuenta(Guid id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            //string url = string.Format(endpoint, id);

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("La cuenta no existe.");
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode} al obtener la cuenta: {errorContent}");
            }

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var cuentaResponse = JsonSerializer.Deserialize<CuentaResponse>(resultado, options);

            if (cuentaResponse != null)
            {
                Cuenta.idCuenta = cuentaResponse.idCuenta;
                Cuenta.Nombre = cuentaResponse.Nombre ?? string.Empty;
                Cuenta.Descripcion = cuentaResponse.Descripcion ?? string.Empty;
                Cuenta.idCategoria = cuentaResponse.idCategoria;
                Cuenta.PermitirSalarioNegativo = cuentaResponse.PermitirSalarioNegativo;                
                Cuenta.FechaCreacion = cuentaResponse.FechaCreacion == DateTime.MinValue ? DateTime.Now : cuentaResponse.FechaCreacion;
                Cuenta.FechaUltimaModificacion = cuentaResponse.FechaUltimaModificacion == DateTime.MinValue ? DateTime.Now : cuentaResponse.FechaUltimaModificacion;
                Cuenta.Estado = cuentaResponse.Estado;
                Cuenta.IdUsuario = Guid.Parse("3A9C3399-15F3-4123-BEB8-96CB45AEB18D");
            }
            else
            {
                throw new Exception("No se pudo deserializar la respuesta de la API.");
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
                Categorias = new SelectList(new List<CategoriaResponse>(), "idCategoria", "Nombre");
            }
        }

        private async Task CargarCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode} al obtener categorías: {errorContent}");
            }

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var categorias = JsonSerializer.Deserialize<IList<CategoriaResponse>>(resultado, options) ?? new List<CategoriaResponse>();
            
            Categorias = new SelectList(categorias.Where(c => c.Estado), "idCategoria", "Nombre", Cuenta.idCategoria);
        }
    }
}
