using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class EstadoReserva
    {
        [Key]
        public int IdEstadoReserva { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty; // Pendiente, Confirmada, Cancelada

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
