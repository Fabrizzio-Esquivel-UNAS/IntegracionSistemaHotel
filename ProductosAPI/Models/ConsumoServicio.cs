using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class ConsumoServicio
    {
        [Key]
        public int IdConsumo { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; } // Precio al momento de la compra
        public DateTime FechaConsumo { get; set; } = DateTime.Now;

        // Claves foráneas
        public int IdReserva { get; set; }
        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }

        public int IdServicio { get; set; }
        [ForeignKey("IdServicio")]
        public virtual Servicio? Servicio { get; set; }
    }
}
