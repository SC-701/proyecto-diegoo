using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class EditarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaResponse cuenta { get; set; } = default!;
        public CuentaRequest cuentaRequest { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = default!;

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
                var cliente = new HttpClient();

                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    // Cargar categorías primero
                    await ObtenerCategoriasAsync();

                    // Cargar datos de la cuenta
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    cuenta = JsonSerializer.Deserialize<CuentaResponse>(resultado, opciones);

                    if (cuenta != null)
                    {
                        // Seleccionar la categoría actual
                        categoriaSeleccionada = cuenta.idCategoria;

                        // Marcar la categoría seleccionada en la lista
                        var categoriaActual = categorias.FirstOrDefault(c => Guid.Parse(c.Value) == cuenta.idCategoria);
                        if (categoriaActual != null)
                        {
                            categoriaActual.Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al cargar la cuenta: {ex.Message}");

                try
                {
                    await ObtenerCategoriasAsync();
                }
                catch
                {
                    categorias = new List<SelectListItem>();
                }
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (cuenta.idCuenta == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await ObtenerCategoriasAsync();
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
                HttpClient cliente = new HttpClient();

                CuentaRequest cuentaRequest = new CuentaRequest
                {
                    idCuenta = cuenta.idCuenta,
                    Nombre = cuenta.Nombre,
                    Descripcion = cuenta.Descripcion,
                    idCategoria = categoriaSeleccionada,
                    PermitirSalarioNegativo = cuenta.PermitirSalarioNegativo,
                    FechaCreacion = cuenta.FechaCreacion,
                    FechaUltimaModificacion = DateTime.Now,
                    Estado = cuenta.Estado,
                    IdUsuario = Guid.Parse("3A9C3399-15F3-4123-BEB8-96CB45AEB18D") 
                };

                var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, cuenta.idCuenta.ToString()), cuentaRequest);
                respuesta.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Cuenta actualizada exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar la cuenta: {ex.Message}");
                await ObtenerCategoriasAsync();
                return Page();
            }
        }

        private async Task ObtenerCategoriasAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
                HttpClient cliente = new HttpClient();
                HttpRequestMessage solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                HttpResponseMessage respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    string resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultadoDeserializado = JsonSerializer.Deserialize<List<CategoriaResponse>>(resultado, opciones);

                    categorias = resultadoDeserializado?.Where(c => c.Estado)
                        .Select(a => new SelectListItem
                        {
                            Value = a.idCategoria.ToString(),
                            Text = a.Nombre.ToString()
                        }).ToList() ?? new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                categorias = new List<SelectListItem>();
                ModelState.AddModelError(string.Empty, $"Error al cargar categorías: {ex.Message}");
            }
        }
    }
}
