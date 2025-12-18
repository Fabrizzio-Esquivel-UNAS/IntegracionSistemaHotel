using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductosAPI.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del rol debe tener entre 3 y 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: Administrador, Recepcionista

        // Relacion: Un rol lo tienen muchos usuarios
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
