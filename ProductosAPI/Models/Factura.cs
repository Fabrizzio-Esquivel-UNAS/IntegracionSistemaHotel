using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio.")]
        [Range(0.00, double.MaxValue, ErrorMessage = "El monto total debe ser mayor o igual a 0.")]
        public decimal MontoTotal { get; set; } // Suma de habitacion + consumos
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        // Relacion 1 a 1 con Reserva usualmente
        [Required(ErrorMessage = "La reserva es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una reserva v?lida.")]
        public int IdReserva { get; set; }
        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }
    }
}
