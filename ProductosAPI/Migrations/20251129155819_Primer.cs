using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Primer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaHabitacion",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaHabitacion", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "EstadoReserva",
                columns: table => new
                {
                    IdEstadoReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoReserva", x => x.IdEstadoReserva);
                });

            migrationBuilder.CreateTable(
                name: "Huesped",
                columns: table => new
                {
                    IdHuesped = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentoIdentidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huesped", x => x.IdHuesped);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroHabitacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: false),
                    EstaDisponible = table.Column<bool>(type: "bit", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.IdHabitacion);
                    table.ForeignKey(
                        name: "FK_Habitacion_CategoriaHabitacion_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "CategoriaHabitacion",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHuesped = table.Column<int>(type: "int", nullable: false),
                    IdHabitacion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdEstadoReserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reserva_EstadoReserva_IdEstadoReserva",
                        column: x => x.IdEstadoReserva,
                        principalTable: "EstadoReserva",
                        principalColumn: "IdEstadoReserva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Habitacion_IdHabitacion",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitacion",
                        principalColumn: "IdHabitacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Huesped_IdHuesped",
                        column: x => x.IdHuesped,
                        principalTable: "Huesped",
                        principalColumn: "IdHuesped",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumoServicio",
                columns: table => new
                {
                    IdConsumo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaConsumo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdReserva = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumoServicio", x => x.IdConsumo);
                    table.ForeignKey(
                        name: "FK_ConsumoServicio_Reserva_IdReserva",
                        column: x => x.IdReserva,
                        principalTable: "Reserva",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumoServicio_Servicio_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Reserva_IdReserva",
                        column: x => x.IdReserva,
                        principalTable: "Reserva",
                        principalColumn: "IdReserva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriaHabitacion",
                columns: new[] { "IdCategoria", "Descripcion", "Nombre", "PrecioBase" },
                values: new object[,]
                {
                    { 1, "Habitación para una persona.", "Simple", 50.00m },
                    { 2, "Habitación para dos personas.", "Doble", 80.00m },
                    { 3, "Suite de lujo con sala de estar.", "Suite", 150.00m }
                });

            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "IdEstadoReserva", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Confirmada" },
                    { 3, "Cancelada" },
                    { 4, "Completada" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Recepcionista" }
                });

            migrationBuilder.InsertData(
                table: "Servicio",
                columns: new[] { "IdServicio", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "Servicio a la habitación", 15.00m },
                    { 2, "Lavandería", 25.00m },
                    { 3, "Minibar", 10.00m }
                });

            migrationBuilder.InsertData(
                table: "Habitacion",
                columns: new[] { "IdHabitacion", "EstaDisponible", "IdCategoria", "NumeroHabitacion", "Piso" },
                values: new object[,]
                {
                    { 1, true, 1, "101", 1 },
                    { 2, true, 1, "102", 1 },
                    { 3, true, 2, "201", 2 },
                    { 4, false, 2, "202", 2 },
                    { 5, true, 3, "301", 3 }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Correo", "EstaActivo", "IdRol", "NombreCompleto", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "admin@example.com", true, 1, "Admin User", "$2a$11$qMa1JzKkm99KOmBdOv82qO15gSVbiKZlxIgYkqGQ67z0HW4GGncnq" },
                    { 2, "recepcionista@example.com", true, 2, "Receptionist User", "$2a$11$azWOhJcRYYhNDBB2xCVFhe0Wuya3kemkgm02BHusCRv3vjFzhkRRW" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoServicio_IdReserva",
                table: "ConsumoServicio",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoServicio_IdServicio",
                table: "ConsumoServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdReserva",
                table: "Factura",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_IdCategoria",
                table: "Habitacion",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdEstadoReserva",
                table: "Reserva",
                column: "IdEstadoReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdHabitacion",
                table: "Reserva",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdHuesped",
                table: "Reserva",
                column: "IdHuesped");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdUsuario",
                table: "Reserva",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumoServicio");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "EstadoReserva");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Huesped");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CategoriaHabitacion");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
