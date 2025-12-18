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
    public class ConsumoServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumoServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ConsumoServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoServicio>>> GetConsumoServicios()
        {
            try
            {
                return await _context.ConsumoServicio.Include(c => c.Reserva).Include(c => c.Servicio).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/ConsumoServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoServicio>> GetConsumoServicio(int id)
        {
            try
            {
                var consumoServicio = await _context.ConsumoServicio.Include(c => c.Reserva).Include(c => c.Servicio).FirstOrDefaultAsync(c => c.IdConsumo == id);

                if (consumoServicio == null)
                {
                    return NotFound(new { message = "Consumo de servicio no encontrado" });
                }

                return consumoServicio;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/ConsumoServicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumoServicio(int id, ConsumoServicio consumoServicio)
        {

            _context.Entry(consumoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumoServicioExists(id))
                {
                    return NotFound(new { message = "Consumo de servicio no encontrado" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar el consumo de servicio" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/ConsumoServicio
        [HttpPost]
        public async Task<ActionResult<ConsumoServicio>> PostConsumoServicio(ConsumoServicio consumoServicio)
        {
            try
            {
                _context.ConsumoServicio.Add(consumoServicio);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetConsumoServicio", new { id = consumoServicio.IdConsumo }, consumoServicio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/ConsumoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumoServicio(int id)
        {
            try
            {
                var consumoServicio = await _context.ConsumoServicio.FindAsync(id);
                if (consumoServicio == null)
                {
                    return NotFound(new { message = "Consumo de servicio no encontrado" });
                }

                _context.ConsumoServicio.Remove(consumoServicio);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool ConsumoServicioExists(int id)
        {
            return _context.ConsumoServicio.Any(e => e.IdConsumo == id);
        }
    }
}
