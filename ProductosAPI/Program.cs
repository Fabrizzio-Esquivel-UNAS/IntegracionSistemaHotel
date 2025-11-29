using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductosAPI.Data;
using Microsoft.OpenApi.Models;

namespace ProductosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodo",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Valida el emisor, asegura que el token proviene de un emisor confiable
                        ValidateIssuer = true,
                        //Valida la audiencia, el token fue generado para esta aplicación y no para otra
                        ValidateAudience = true,
                        //Valida el tiempo de vida del token
                        ValidateLifetime = true,
                        //valida la firma del emisor
                        ValidateIssuerSigningKey = true,
                        //define quién es el emisor
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        //define quién será la audiencia
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        //Se proporciona la clave secreta que se va a usar para validar la firma
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT 'Jwt:Key' no se encontró en la configuración.")))
                    };
                });

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Productos API",
                    Version = "v1",
                    Description = "API REST para gestión de productos " +
                    "y categorías con autenticación JWT"
                });

                //Definir el esquema de seguridad del Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Autorización por token",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingresa tu token JWT aquí. Formato: Bearer {token}"
                });

                //Exigir ese esquema de autorización por defecto en las operaciones
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<String>()
                    }
                });
            });

            builder.Services.AddAuthorization(options =>
            {
                // Política: PuedeAgregarProductos
                options.AddPolicy("PuedeAgregarProductos", policy =>
                    policy.RequireClaim("PuedeAgregarProductos", "True"));

                // Política: PuedeModificarProductos
                options.AddPolicy("PuedeModificarProductos", policy =>
                    policy.RequireClaim("PuedeModificarProductos", "True"));

                // Política: PuedeEliminarProductos
                options.AddPolicy("PuedeEliminarProductos", policy =>
                    policy.RequireClaim("PuedeEliminarProductos", "True"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("PermitirTodo");

            // Seed the database
            SeedData.Initialize(app);

            app.MapControllers();

            app.Run();
        }
    }
}