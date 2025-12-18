using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosAPI.Models
{
    public class Habitacion
    {
        [Key]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El n?mero de habitaci?n es obligatorio.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "El n?mero de habitaci?n debe tener entre 1 y 10 caracteres.")]
        public string NumeroHabitacion { get; set; } = string.Empty; // Ejemplo: "101", "205B"

        [Required(ErrorMessage = "El piso es obligatorio.")]
        [Range(1, 100, ErrorMessage = "El piso debe estar entre 1 y 100.")]
        public int Piso { get; set; }

        public bool EstaDisponible { get; set; } = true;

        // Clave foranea
        [Required(ErrorMessage = "La categor?a es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categor?a v?lida.")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual CategoriaHabitacion? CategoriaHabitacion { get; set; }
    }
}
