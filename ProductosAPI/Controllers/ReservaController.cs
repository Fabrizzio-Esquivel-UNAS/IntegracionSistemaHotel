using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;
using ProductosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            try
            {
                return await _context.Reserva
                    .Include(r => r.Huesped)
                    .Include(r => r.Habitacion)
                    .Include(r => r.Usuario)
                    .Include(r => r.EstadoReserva)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            try
            {
                var reserva = await _context.Reserva
                    .Include(r => r.Huesped)
                    .Include(r => r.Habitacion)
                    .Include(r => r.Usuario)
                    .Include(r => r.EstadoReserva)
                    .FirstOrDefaultAsync(r => r.IdReserva == id);

                if (reserva == null)
                {
                    return NotFound(new { message = "Reserva no encontrada" });
                }

                return reserva;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Reserva/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound(new { message = "Reserva no encontrada" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar la reserva" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Reserva
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            try
            {
                _context.Reserva.Add(reserva);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReserva", new { id = reserva.IdReserva }, reserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            try
            {
                var reserva = await _context.Reserva.FindAsync(id);
                if (reserva == null)
                {
                    return NotFound(new { message = "Reserva no encontrada" });
                }

                _context.Reserva.Remove(reserva);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.IdReserva == id);
        }
    }
}
