using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosAPI.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [NotMapped]
        public int Id
        {
            get => IdUsuario;
            set => IdUsuario = value;
        }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        // Mantener PasswordHash con el mismo nombre pedido
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string PasswordHash { get; set; } = string.Empty;

        public bool EstaActivo { get; set; } = true;

        // Clave foranea al rol
        [Required(ErrorMessage = "El rol es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol válido.")]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }

        // Compatibilidad con codigo existente que usaba Username
        [NotMapped]
        public string Username
        {
            get => Correo;
            set => Correo = value;
        }
    }
}
