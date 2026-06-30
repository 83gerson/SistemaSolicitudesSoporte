using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaSolicitudesSoporte.Models;

namespace SistemaSolicitudesSoporte.Controllers
{
    public class SolicitudesController : Controller
    {
        // Listas fijas usadas
        private static readonly List<string> Categorias =
            new List<string> { "Hardware", "Software", "Red", "Otro" };

        private static readonly List<string> Estados =
            new List<string> { "Pendiente", "En Proceso", "Resuelto" };

        // GET: Solicitudes
        // Lista todas las solicitudes guardadas en la sesión del usuario
        public ActionResult Index()
        {
            var solicitudes = SolicitudRepository.ObtenerSolicitudes(Session);

            ViewBag.Categorias = new SelectList(Categorias);
            ViewBag.Estados = new SelectList(Estados);

            return View(solicitudes);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.Categorias = new SelectList(Categorias);
            return View();
        }

        // POST: Solicitudes/Create
        // Recibe el formulario, valida el modelo y si todo está bien,
        // agrega la solicitud a la lista en memoria (Session) y muestra
        // un mensaje de confirmación por medio de TempData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                solicitud.Estado = "Pendiente";
                solicitud.FechaRegistro = DateTime.Now;

                SolicitudRepository.AgregarSolicitud(Session, solicitud);

                TempData["Mensaje"] = "La solicitud se registró correctamente.";
                return RedirectToAction("Index");
            }

            // Si no es válido, se vuelve a mostrar el formulario con los mensajes de validación y los datos que el usuario digitó
            ViewBag.Categorias = new SelectList(Categorias);
            return View(solicitud);
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int id)
        {
            var solicitudes = SolicitudRepository.ObtenerSolicitudes(Session);
            var solicitud = solicitudes.FirstOrDefault(s => s.Id == id);

            if (solicitud == null)
            {
                TempData["MensajeError"] = "No se encontró la solicitud solicitada.";
                return RedirectToAction("Index");
            }

            return View(solicitud);
        }

        // GET: Solicitudes/FiltrarAjax
        // Acción desde JavaScript mediante AJAX. Devuelve un JSON con las solicitudes filtradas, sin tener que recargar la página
        [HttpGet]
        public JsonResult FiltrarAjax(string categoria, string estado)
        {
            var solicitudes = SolicitudRepository.ObtenerSolicitudes(Session);

            var resultado = solicitudes
                .Where(s =>
                    (string.IsNullOrEmpty(categoria) || categoria == "Todas" || s.Categoria == categoria) &&
                    (string.IsNullOrEmpty(estado) || estado == "Todos" || s.Estado == estado))
                .Select(s => new
                {
                    s.Id,
                    s.NombreSolicitante,
                    s.Categoria,
                    s.Estado,
                    FechaRegistro = s.FechaRegistro.ToString("dd/MM/yyyy HH:mm")
                })
                .ToList();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}
