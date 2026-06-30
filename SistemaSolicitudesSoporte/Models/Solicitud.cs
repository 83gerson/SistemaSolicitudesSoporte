using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaSolicitudesSoporte.Models
{
    /// <summary>
    /// Representa una solicitud de soporte técnico registrada por un usuario.
    /// Esta clase es el Modelo dentro del patrón MVC, ya que define los datos
    /// y las reglas de validación, pero no contiene lógica de presentación ni de acceso a los datos
    /// </summary>
    public class Solicitud
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del solicitante es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre del solicitante")]
        public string NombreSolicitante { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La descripción del problema es obligatoria.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción del problema")]
        public string Descripcion { get; set; }

        // El estado no lo digita el usuario, lo asigna el controlador poniendo siempre pendiente
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }
    }
}
