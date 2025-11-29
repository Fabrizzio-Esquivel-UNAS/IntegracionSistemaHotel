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

        [Required]
        [MaxLength(100)]
        public string Correo { get; set; } = string.Empty;

        // Mantener PasswordHash con el mismo nombre pedido
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool EstaActivo { get; set; } = true;

        // Clave foránea al rol
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }

        // Compatibilidad con código existente que usaba Username
        [NotMapped]
        public string Username
        {
            get => Correo;
            set => Correo = value;
        }
    }
}
