using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }
        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder los 500 caracteres.")]
        public string? Observaciones { get; set; }

        // Claves foraneas y Relaciones
        [Required(ErrorMessage = "El hu?sped es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un hu?sped v?lido.")]
        public int IdHuesped { get; set; }
        [ForeignKey("IdHuesped")]
        public virtual Huesped? Huesped { get; set; }

        [Required(ErrorMessage = "La habitaci?n es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una habitaci?n v?lida.")]
        public int IdHabitacion { get; set; }
        [ForeignKey("IdHabitacion")]
        public virtual Habitacion? Habitacion { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario v?lido.")]
        public int IdUsuario { get; set; } // Empleado que registro
        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado v?lido.")]
        public int IdEstadoReserva { get; set; }
        [ForeignKey("IdEstadoReserva")]
        public virtual EstadoReserva? EstadoReserva { get; set; }
    }
}
