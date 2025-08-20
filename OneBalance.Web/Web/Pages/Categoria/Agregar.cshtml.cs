using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Categoria
{
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CategoriaRequest Categoria { get; set; }

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet()
        {
            Categoria.Estado = true;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCategoria");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, Categoria);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}