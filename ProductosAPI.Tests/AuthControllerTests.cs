using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductosAPI.Controllers;
using ProductosAPI.Data;
using ProductosAPI.Models;
using Xunit;

namespace ProductosAPI.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AppDbContext _context;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test
                .Options;

            _context = new AppDbContext(options);
            _mockConfig = new Mock<IConfiguration>();
            
            // Setup config for JWT
            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("supersecretkey12345678901234567890");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            _controller = new AuthController(_context, _mockConfig.Object);
        }

        [Fact]
        public async Task Registro_ExistingUser_ReturnsBadRequest()
        {
            // Arrange
            _context.Usuario.Add(new Usuario { Correo = "existing@test.com", PasswordHash = "hash", IdRol = 1 });
            await _context.SaveChangesAsync();

            var dto = new AuthController.RegistroDto
            {
                Username = "existing@test.com",
                Password = "password"
            };

            // Act
            var result = await _controller.Registro(dto);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var value = actionResult.Value;
            var messageProperty = value.GetType().GetProperty("message");
            var message = messageProperty.GetValue(value, null) as string;
            
            Assert.Equal("El nombre de usuario ya existe", message);
        }

        [Fact]
        public async Task Registro_NewUser_ReturnsOk()
        {
            // Arrange
            var dto = new AuthController.RegistroDto
            {
                Username = "new@test.com",
                Password = "password",
                Rol = "Recepcionista"
            };

            // Act
            var result = await _controller.Registro(dto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = actionResult.Value;
            var messageProperty = value.GetType().GetProperty("Message");
            var message = messageProperty.GetValue(value, null) as string;

            Assert.Equal("Usuario registrado exitosamente", message);
            
            // Verify user created
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == "new@test.com");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var dto = new AuthController.LoginDto
            {
                Username = "nonexistent@test.com",
                Password = "password"
            };

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
            var value = actionResult.Value;
            var messageProperty = value.GetType().GetProperty("message");
            var message = messageProperty.GetValue(value, null) as string;

            Assert.Contains("Credenciales inv", message);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            string password = "password";
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            
            var role = new Rol { Nombre = "Administrador" };
            _context.Rol.Add(role);
            await _context.SaveChangesAsync();

            var user = new Usuario 
            { 
                Correo = "valid@test.com", 
                PasswordHash = hash, 
                IdRol = role.IdRol 
            };
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();

            var dto = new AuthController.LoginDto
            {
                Username = "valid@test.com",
                Password = password
            };

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = actionResult.Value;
            var tokenProperty = value.GetType().GetProperty("token");
            var token = tokenProperty.GetValue(value, null) as string;

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}
