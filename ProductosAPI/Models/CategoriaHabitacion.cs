using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class CategoriaHabitacion
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: Simple, Matrimonial, Suite

        [Required]
        public decimal PrecioBase { get; set; } // Usamos decimal para dinero

        public string? Descripcion { get; set; }

        public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}
