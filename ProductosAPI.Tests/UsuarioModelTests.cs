using System.ComponentModel.DataAnnotations;
using ProductosAPI.Models;
using Xunit;

namespace ProductosAPI.Tests
{
    public class UsuarioModelTests
    {
        [Fact]
        public void Usuario_ValidModel_ReturnsNoErrors()
        {
            var usuario = new Usuario
            {
                Correo = "test@example.com",
                PasswordHash = "hashedpassword",
                IdRol = 1
            };

            var results = ValidationHelper.ValidateModel(usuario);

            Assert.Empty(results);
        }

        [Fact]
        public void Usuario_MissingCorreo_ReturnsError()
        {
            var usuario = new Usuario
            {
                PasswordHash = "hashedpassword",
                IdRol = 1
            };

            var results = ValidationHelper.ValidateModel(usuario);

            Assert.Contains(results, r => r.MemberNames.Contains("Correo") && r.ErrorMessage.Contains("El correo es obligatorio"));
        }

        [Fact]
        public void Usuario_InvalidEmail_ReturnsError()
        {
            var usuario = new Usuario
            {
                Correo = "invalid-email",
                PasswordHash = "hashedpassword",
                IdRol = 1
            };

            var results = ValidationHelper.ValidateModel(usuario);

            Assert.Contains(results, r => r.MemberNames.Contains("Correo") && r.ErrorMessage.Contains("El formato del correo"));
        }

        [Fact]
        public void Usuario_MissingPasswordHash_ReturnsError()
        {
            var usuario = new Usuario
            {
                Correo = "test@example.com",
                IdRol = 1
            };

            var results = ValidationHelper.ValidateModel(usuario);

            Assert.Contains(results, r => r.MemberNames.Contains("PasswordHash") && r.ErrorMessage.Contains("La contrase"));
        }

        [Fact]
        public void Usuario_InvalidIdRol_ReturnsError()
        {
            var usuario = new Usuario
            {
                Correo = "test@example.com",
                PasswordHash = "hashedpassword",
                IdRol = 0
            };

            var results = ValidationHelper.ValidateModel(usuario);

            Assert.Contains(results, r => r.MemberNames.Contains("IdRol") && r.ErrorMessage.Contains("Debe seleccionar un rol"));
        }
    }
}
