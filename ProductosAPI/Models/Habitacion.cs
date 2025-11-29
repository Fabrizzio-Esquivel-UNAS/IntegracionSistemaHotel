using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosAPI.Models
{
    public class Habitacion
    {
        [Key]
        public int IdHabitacion { get; set; }

        [Required]
        [MaxLength(10)]
        public string NumeroHabitacion { get; set; } = string.Empty; // Ejemplo: "101", "205B"

        public int Piso { get; set; }

        public bool EstaDisponible { get; set; } = true;

        // Clave foránea
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual CategoriaHabitacion? CategoriaHabitacion { get; set; }
    }
}
