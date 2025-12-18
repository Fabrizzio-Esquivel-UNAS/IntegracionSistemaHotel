using System.ComponentModel.DataAnnotations;

namespace ProductosAPI.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del servicio debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: Servicio a la habitacion

        [Required(ErrorMessage = "El precio del servicio es obligatorio.")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio debe ser mayor a 0 y menor a 10000.")]
        public decimal Precio { get; set; }
    }
}
