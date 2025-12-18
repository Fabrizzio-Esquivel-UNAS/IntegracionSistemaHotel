using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class CategoriaHabitacion
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categor?a es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la categor?a debe tener entre 3 y 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: Simple, Matrimonial, Suite

        [Required(ErrorMessage = "El precio base es obligatorio.")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio base debe ser mayor a 0 y menor a 10000.")]
        public decimal PrecioBase { get; set; } // Usamos decimal para dinero

        [StringLength(500, ErrorMessage = "La descripci?n no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}
