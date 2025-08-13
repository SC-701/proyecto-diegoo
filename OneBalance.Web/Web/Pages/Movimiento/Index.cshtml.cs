using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Movimiento
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IConfiguracion _configuracion;
        
        public IList<MovimientoResponse> movimientos { get; set; } = new List<MovimientoResponse>();
        public Guid? IdCuenta { get; set; }
        public string NombreCuenta { get; set; } = string.Empty;
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, IConfiguracion configuracion)
        {
            _logger = logger;
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            IdCuenta = id;

            if (id == null || id == Guid.Empty)
            {
                HasError = true;
                ErrorMessage = "El ID de la cuenta no puede estar vacío.";
                return;
            }

            try
            {
                // Cargar información de la cuenta
                await CargarInformacionCuenta(id.Value);
                
                // Cargar movimientos
                await CargarMovimientos(id.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar movimientos para la cuenta {CuentaId}", id);
                HasError = true;
                ErrorMessage = "Error al cargar los movimientos de la cuenta.";
            }
        }

        private async Task CargarInformacionCuenta(Guid idCuenta)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
                string url = string.Format(endpoint, idCuenta);

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string resultado = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    
                    var cuenta = JsonSerializer.Deserialize<CuentaResponse>(resultado, options);
                    if (cuenta != null)
                    {
                        NombreCuenta = cuenta.Nombre;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "No se pudo cargar la información de la cuenta {CuentaId}", idCuenta);
                NombreCuenta = "Información de cuenta no disponible";
            }
        }

        private async Task CargarMovimientos(Guid idCuenta)
        {
            try
            {
                string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "Ultimos5Movimientos");
                string url = string.Format(endpoint, idCuenta);

                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string resultado = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    
                    var movimientosResult = JsonSerializer.Deserialize<IList<MovimientoResponse>>(resultado, options);
                    movimientos = movimientosResult ?? new List<MovimientoResponse>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // No hay movimientos, pero no es un error
                    movimientos = new List<MovimientoResponse>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Error de red al obtener movimientos para la cuenta {CuentaId}", idCuenta);
                // No lanzar excepción, simplemente no hay movimientos disponibles
                movimientos = new List<MovimientoResponse>();
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "Error al deserializar movimientos para la cuenta {CuentaId}", idCuenta);
                movimientos = new List<MovimientoResponse>();
            }
        }
    }
}