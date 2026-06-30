using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SistemaSolicitudesSoporte.Models
{
    /// <summary>
    /// Simula el acceso a datos de la aplicación mediante una lista en memoria
    /// Mientras la sesión del usuario esté activa, las solicitudes registradas
    /// se mantienen disponibles aunque se navegue entre las páginas. Si la sesión
    /// expira o se reinicia el navegador, la lista vuelve a su estado inicial.
    /// </summary>
    public static class SolicitudRepository
    {
        private const string SessionKey = "ListaSolicitudes";

        public static List<Solicitud> ObtenerSolicitudes(HttpSessionStateBase session)
        {
            var lista = session[SessionKey] as List<Solicitud>;

            if (lista == null)
            {
                lista = CrearDatosIniciales();
                session[SessionKey] = lista;
            }

            return lista;
        }

        public static void AgregarSolicitud(HttpSessionStateBase session, Solicitud solicitud)
        {
            var lista = ObtenerSolicitudes(session);

            solicitud.Id = lista.Count == 0 ? 1 : lista.Max(s => s.Id) + 1;
            lista.Add(solicitud);

            session[SessionKey] = lista;
        }

        // Datos de ejemplo para que la lista no inicie vacía.
        private static List<Solicitud> CrearDatosIniciales()
        {
            return new List<Solicitud>
            {
                new Solicitud
                {
                    Id = 1,
                    NombreSolicitante = "Andres Aguilar",
                    Email = "andres@gmail.com",
                    Categoria = "Hardware",
                    Descripcion = "El equipo de cómputo no enciende desde esta mañana.",
                    Estado = "Resuelto",
                    FechaRegistro = System.DateTime.Now.AddDays(-2)
                },
                new Solicitud
                {
                    Id = 2,
                    NombreSolicitante = "Piero Borja",
                    Email = "piero@gmail.com",
                    Categoria = "Software",
                    Descripcion = "El sistema de facturación se cierra inesperadamente.",
                    Estado = "En Proceso",
                    FechaRegistro = System.DateTime.Now.AddDays(-1)
                }
            };
        }
    }
}
