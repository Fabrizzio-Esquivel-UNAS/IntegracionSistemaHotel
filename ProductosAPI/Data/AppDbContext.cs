using Microsoft.EntityFrameworkCore;
using ProductosAPI.Models;

namespace ProductosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<CategoriaHabitacion> CategoriaHabitacion { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Huesped> Huesped { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<ConsumoServicio> ConsumoServicio { get; set; }
        public DbSet<Factura> Factura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, Nombre = "Administrador" },
                new Rol { IdRol = 2, Nombre = "Recepcionista" }
            );

            // Seed Usuario
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    IdUsuario = 1,
                    Correo = "admin@example.com",
                    PasswordHash = "$2a$11$qMa1JzKkm99KOmBdOv82qO15gSVbiKZlxIgYkqGQ67z0HW4GGncnq", // Password: admin123
                    EstaActivo = true,
                    IdRol = 1
                },
                new Usuario
                {
                    IdUsuario = 2,
                    Correo = "recepcionista@example.com",
                    PasswordHash = "$2a$11$azWOhJcRYYhNDBB2xCVFhe0Wuya3kemkgm02BHusCRv3vjFzhkRRW", // Password: recep123
                    EstaActivo = true,
                    IdRol = 2
                }
            );

            // Seed CategoriaHabitacion
            modelBuilder.Entity<CategoriaHabitacion>().HasData(
                new CategoriaHabitacion { IdCategoria = 1, Nombre = "Simple", PrecioBase = 50.00m, Descripcion = "Habitación para una persona." },
                new CategoriaHabitacion { IdCategoria = 2, Nombre = "Doble", PrecioBase = 80.00m, Descripcion = "Habitación para dos personas." },
                new CategoriaHabitacion { IdCategoria = 3, Nombre = "Suite", PrecioBase = 150.00m, Descripcion = "Suite de lujo con sala de estar." }
            );

            // Seed Habitacion
            modelBuilder.Entity<Habitacion>().HasData(
                new Habitacion { IdHabitacion = 1, NumeroHabitacion = "101", Piso = 1, EstaDisponible = true, IdCategoria = 1 },
                new Habitacion { IdHabitacion = 2, NumeroHabitacion = "102", Piso = 1, EstaDisponible = true, IdCategoria = 1 },
                new Habitacion { IdHabitacion = 3, NumeroHabitacion = "201", Piso = 2, EstaDisponible = true, IdCategoria = 2 },
                new Habitacion { IdHabitacion = 4, NumeroHabitacion = "202", Piso = 2, EstaDisponible = false, IdCategoria = 2 },
                new Habitacion { IdHabitacion = 5, NumeroHabitacion = "301", Piso = 3, EstaDisponible = true, IdCategoria = 3 }
            );

            // Seed EstadoReserva
            modelBuilder.Entity<EstadoReserva>().HasData(
                new EstadoReserva { IdEstadoReserva = 1, Nombre = "Pendiente" },
                new EstadoReserva { IdEstadoReserva = 2, Nombre = "Confirmada" },
                new EstadoReserva { IdEstadoReserva = 3, Nombre = "Cancelada" },
                new EstadoReserva { IdEstadoReserva = 4, Nombre = "Completada" }
            );

            // Seed Servicio
            modelBuilder.Entity<Servicio>().HasData(
                new Servicio { IdServicio = 1, Nombre = "Servicio a la habitación", Precio = 15.00m },
                new Servicio { IdServicio = 2, Nombre = "Lavandería", Precio = 25.00m },
                new Servicio { IdServicio = 3, Nombre = "Minibar", Precio = 10.00m }
            );
        }
    }
}
