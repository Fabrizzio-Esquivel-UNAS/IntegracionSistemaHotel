using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class Huesped
    {
        [Key]
        public int IdHuesped { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre completo debe tener entre 3 y 100 caracteres.")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El documento de identidad debe tener entre 5 y 20 caracteres.")]
        public string DocumentoIdentidad { get; set; } = string.Empty; // DNI o Pasaporte

        [Phone(ErrorMessage = "El formato del tel?fono no es v?lido.")]
        [StringLength(20, ErrorMessage = "El tel?fono no puede exceder los 20 caracteres.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo no es v?lido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string? Correo { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
