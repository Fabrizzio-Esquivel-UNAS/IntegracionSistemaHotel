using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;

namespace ProductosAPI
{
    public static class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (context == null)
                {
                    return;
                }

                // Ensure the database is created.
                context.Database.Migrate();

                // You can add more complex seeding logic here if needed,
                // especially for data that depends on other data being present.
                // For example, creating Huesped, Reserva, ConsumoServicio, and Factura.
            }
        }
    }
}
