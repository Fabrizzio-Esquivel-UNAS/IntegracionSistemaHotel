using System.ComponentModel.DataAnnotations;

namespace ProductosAPI.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: Servicio a la habitación

        [Required]
        public decimal Precio { get; set; }
    }
}
