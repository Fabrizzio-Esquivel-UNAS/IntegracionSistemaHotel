using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ProductosAPI.Models
{
    public class ConsumoServicio
    {
        [Key]
        public int IdConsumo { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, 100, ErrorMessage = "La cantidad debe estar entre 1 y 100.")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio unitario debe ser mayor a 0 y menor a 10000.")]
        public decimal PrecioUnitario { get; set; } // Precio al momento de la compra
        public DateTime FechaConsumo { get; set; } = DateTime.Now;

        // Claves foraneas
        [Required(ErrorMessage = "La reserva es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una reserva v?lida.")]
        public int IdReserva { get; set; }
        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }

        [Required(ErrorMessage = "El servicio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un servicio v?lido.")]
        public int IdServicio { get; set; }
        [ForeignKey("IdServicio")]
        public virtual Servicio? Servicio { get; set; }
    }
}
