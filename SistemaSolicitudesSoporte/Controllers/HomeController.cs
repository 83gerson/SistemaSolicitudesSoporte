using System.Web.Mvc;

namespace SistemaSolicitudesSoporte.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        // Muestra la página de bienvenida, si ya existe un nombre guardado en Session, se le da la bienvenida al usuario
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = Session["NombreUsuario"] as string;
            return View();
        }

        // POST: Home/GuardarNombre
        // Guarda el nombre del usuario en Session para usarlo durante el resto del rato (gestión de estado con Session)
        [HttpPost]
        public ActionResult GuardarNombre(string nombre)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                Session["NombreUsuario"] = nombre;
            }

            return RedirectToAction("Index");
        }
    }
}
