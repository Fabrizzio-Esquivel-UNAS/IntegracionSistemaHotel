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
    public class HabitacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HabitacionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Habitacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetHabitaciones()
        {
            try
            {
                return await _context.Habitacion.Include(h => h.CategoriaHabitacion).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Habitacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacion>> GetHabitacion(int id)
        {
            try
            {
                var habitacion = await _context.Habitacion.Include(h => h.CategoriaHabitacion).FirstOrDefaultAsync(h => h.IdHabitacion == id);

                if (habitacion == null)
                {
                    return NotFound(new { message = "Habitación no encontrada" });
                }

                return habitacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Habitacion/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> PutHabitacion(int id, Habitacion habitacion)
        {

            _context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(id))
                {
                    return NotFound(new { message = "Habitación no encontrada" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar la habitación" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Habitacion
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Habitacion>> PostHabitacion(Habitacion habitacion)
        {
            try
            {
                _context.Habitacion.Add(habitacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHabitacion", new { id = habitacion.IdHabitacion }, habitacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Habitacion/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            try
            {
                var habitacion = await _context.Habitacion.FindAsync(id);
                if (habitacion == null)
                {
                    return NotFound(new { message = "Habitación no encontrada" });
                }

                _context.Habitacion.Remove(habitacion);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacion.Any(e => e.IdHabitacion == id);
        }
    }
}
