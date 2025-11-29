using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public string? Observaciones { get; set; }

        // Claves foráneas y Relaciones
        public int IdHuesped { get; set; }
        [ForeignKey("IdHuesped")]
        public virtual Huesped? Huesped { get; set; }

        public int IdHabitacion { get; set; }
        [ForeignKey("IdHabitacion")]
        public virtual Habitacion? Habitacion { get; set; }

        public int IdUsuario { get; set; } // Empleado que registró
        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }

        public int IdEstadoReserva { get; set; }
        [ForeignKey("IdEstadoReserva")]
        public virtual EstadoReserva? EstadoReserva { get; set; }
    }
}
