using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Controllers;
using ProductosAPI.Data;
using ProductosAPI.Models;
using Xunit;

namespace ProductosAPI.Tests
{
    public class UsuarioControllerTests
    {
        private readonly AppDbContext _context;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new UsuarioController(_context);
        }

        [Fact]
        public async Task GetUsuario_ExistingId_ReturnsUsuario()
        {
            // Arrange
            var rol = new Rol { Nombre = "TestRol" };
            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();

            var usuario = new Usuario { Correo = "test@test.com", PasswordHash = "hash", IdRol = rol.IdRol };
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsuario(usuario.IdUsuario);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Usuario>>(result);
            var returnValue = Assert.IsType<Usuario>(actionResult.Value);
            Assert.Equal(usuario.IdUsuario, returnValue.IdUsuario);
        }

        [Fact]
        public async Task GetUsuario_NonExistingId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetUsuario(999);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var value = actionResult.Value;
            var messageProperty = value.GetType().GetProperty("message");
            var message = messageProperty.GetValue(value, null) as string;

            Assert.Equal("Usuario no encontrado", message);
        }

        [Fact]
        public async Task DeleteUsuario_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var usuario = new Usuario { Correo = "test@test.com", PasswordHash = "hash", IdRol = 1 };
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteUsuario(usuario.IdUsuario);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await _context.Usuario.FindAsync(usuario.IdUsuario));
        }

        [Fact]
        public async Task DeleteUsuario_NonExistingId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.DeleteUsuario(999);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            var value = actionResult.Value;
            var messageProperty = value.GetType().GetProperty("message");
            var message = messageProperty.GetValue(value, null) as string;

            Assert.Equal("Usuario no encontrado", message);
        }
    }
}
