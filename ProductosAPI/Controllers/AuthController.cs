using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductosAPI.Data;
using ProductosAPI.Models;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // DTOs to accept simple input without binding to navigation properties
        public class RegistroDto
        {
            public string Username { get; set; } = string.Empty; // mapped to Usuario.Correo
            public string Password { get; set; } = string.Empty;
            public string? Rol { get; set; }
        }

        public class LoginDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> Registro([FromBody] RegistroDto dto)
        {
            if (await _context.Usuario.AnyAsync(u => u.Correo == dto.Username))
            {
                return BadRequest("El nombre de usuario ya existe");
            }

            // Hashear la contraseña
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            //validamos rol por nombre
            var rolesValidos = new[] { "Administrador", "Recepcionista" };
            string rolNombre = dto.Rol; 

            if (string.IsNullOrWhiteSpace(rolNombre) || !rolesValidos.Contains(rolNombre))
            {
                rolNombre = "Recepcionista"; // por defecto
            }

            // Buscar rol existente o crearlo
            var role = await _context.Rol.FirstOrDefaultAsync(r => r.Nombre == rolNombre);
            if (role == null)
            {
                role = new Rol { Nombre = rolNombre };
                _context.Rol.Add(role);
                await _context.SaveChangesAsync();
            }

            var user = new Usuario
            {
                Correo = dto.Username,
                PasswordHash = passwordHash,
                IdRol = role.IdRol
            };
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Usuario registrado exitosamente" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == dto.Username);

            //verificar usuario y contraseña
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Credenciales inválidas");
            }

            //Generar el token JWT
            string token = GenerateJwtToken(user);
            return Ok(new { token = token });
        }

        private string GenerateJwtToken(Usuario user)
        {
            var roleNombre = user.Rol?.Nombre ?? string.Empty;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, roleNombre)
            };

            //Inicializamos permisos en "false"
            bool puedeAgregar = false;
            bool puedeModificar = false;
            bool puedeEliminar = false;

            switch (roleNombre)
            {
                case "Administrador":
                    puedeAgregar = true;
                    puedeModificar = true;
                    puedeEliminar = true;
                    break;

                case "Recepcionista":
                    puedeAgregar = true;
                    puedeModificar = true;
                    puedeEliminar = false;
                    break;
            }

            //Claims de permisos
            claims.Add(new Claim("PuedeAgregarProductos", puedeAgregar.ToString()));   // "True"/"False"
            claims.Add(new Claim("PuedeModificarProductos", puedeModificar.ToString()));
            claims.Add(new Claim("PuedeEliminarProductos", puedeEliminar.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Duración del token
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}