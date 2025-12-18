using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class EstadoReserva
    {
        [Key]
        public int IdEstadoReserva { get; set; }

        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del estado debe tener entre 3 y 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Pendiente, Confirmada, Cancelada

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
