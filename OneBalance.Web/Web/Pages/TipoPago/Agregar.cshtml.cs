using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.TipoPago
{
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public TipoPagoRequest TipoPago { get; set; }

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarTipoPago");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, TipoPago);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
