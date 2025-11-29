using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        public decimal MontoTotal { get; set; } // Suma de habitación + consumos
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        // Relación 1 a 1 con Reserva usualmente
        public int IdReserva { get; set; }
        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }
    }
}
