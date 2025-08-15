using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Movimiento
{
    public class EditarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public MovimientoResponse movimiento { get; set; } = default!;
        public MovimientoRequest movimientoRequest { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> cuentas { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> tiposPago { get; set; } = default!;

        [BindProperty]
        public Guid cuentaSeleccionada { get; set; } = default!;

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } = default!;

        [BindProperty]
        public Guid tipoPagoSeleccionado { get; set; } = default!;

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
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerMovimientoPorId");
                var cliente = new HttpClient();

                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    await ObtenerCuentasAsync();
                    await ObtenerCategoriasAsync();
                    await ObtenerTiposPagoAsync();

                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    movimiento = JsonSerializer.Deserialize<MovimientoResponse>(resultado, opciones);

                    if (movimiento != null)
                    {
                        cuentaSeleccionada = movimiento.IdCuenta;
                        categoriaSeleccionada = movimiento.IdCategoria;
                        tipoPagoSeleccionado = movimiento.IdTipoPago;

                        MarcarSeleccionados();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al cargar el movimiento: {ex.Message}");

                try
                {
                    await ObtenerCuentasAsync();
                    await ObtenerCategoriasAsync();
                    await ObtenerTiposPagoAsync();
                }
                catch
                {
                    cuentas = new List<SelectListItem>();
                    categorias = new List<SelectListItem>();
                    tiposPago = new List<SelectListItem>();
                }
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (movimiento.idMovimiento == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await ObtenerCuentasAsync();
                await ObtenerCategoriasAsync();
                await ObtenerTiposPagoAsync();
                return Page();
            }

            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarMovimiento");
                var cliente = new HttpClient();

                var movimientoRequest = new MovimientoRequest
                {
                    idMovimiento = movimiento.idMovimiento,
                    Descripcion = movimiento.Descripcion,
                    MontoOriginal = movimiento.MontoOriginal,
                    Fecha = movimiento.Fecha,
                    Estado = movimiento.Estado,
                    IdCuenta = cuentaSeleccionada,
                    IdCategoria = categoriaSeleccionada,
                    IdTipoPago = tipoPagoSeleccionado
                };

                var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, movimiento.idMovimiento.ToString()), movimientoRequest);
                respuesta.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Movimiento actualizado exitosamente.";
                return RedirectToPage("./Index", new { id = cuentaSeleccionada });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el movimiento: {ex.Message}");
                await ObtenerCuentasAsync();
                await ObtenerCategoriasAsync();
                await ObtenerTiposPagoAsync();
                return Page();
            }
        }

        private async Task ObtenerCuentasAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentasPorUsuario");
                string usuarioId = "3A9C3399-15F3-4123-BEB8-96CB45AEB18D";
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, usuarioId));

                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultadoDeserializado = JsonSerializer.Deserialize<List<CuentaResponse>>(resultado, opciones);

                    cuentas = resultadoDeserializado?.Where(c => c.Estado)
                        .Select(a => new SelectListItem
                        {
                            Value = a.idCuenta.ToString(),
                            Text = a.Nombre.ToString()
                        }).ToList() ?? new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                cuentas = new List<SelectListItem>();
                ModelState.AddModelError(string.Empty, $"Error al cargar cuentas: {ex.Message}");
            }
        }

        private async Task ObtenerCategoriasAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var resultado = await respuesta.Content.ReadAsStringAsync();
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

        private async Task ObtenerTiposPagoAsync()
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTipoPago");
                var cliente = new HttpClient();
                var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

                var respuesta = await cliente.SendAsync(solicitud);
                respuesta.EnsureSuccessStatusCode();

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultadoDeserializado = JsonSerializer.Deserialize<List<TipoPagoResponse>>(resultado, opciones);

                    tiposPago = resultadoDeserializado?.Where(t => t.activestado)
                        .Select(a => new SelectListItem
                        {
                            Value = a.idTipoPago.ToString(),
                            Text = a.nombre.ToString()
                        }).ToList() ?? new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                tiposPago = new List<SelectListItem>();
                ModelState.AddModelError(string.Empty, $"Error al cargar tipos de pago: {ex.Message}");
            }
        }

        private void MarcarSeleccionados()
        {
            var cuentaActual = cuentas.FirstOrDefault(c => Guid.Parse(c.Value) == cuentaSeleccionada);
            if (cuentaActual != null)
                cuentaActual.Selected = true;

            var categoriaActual = categorias.FirstOrDefault(c => Guid.Parse(c.Value) == categoriaSeleccionada);
            if (categoriaActual != null)
                categoriaActual.Selected = true;

            var tipoPagoActual = tiposPago.FirstOrDefault(t => Guid.Parse(t.Value) == tipoPagoSeleccionado);
            if (tipoPagoActual != null)
                tipoPagoActual.Selected = true;
        }
    }
}

