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
            return await _context.ConsumoServicio.Include(c => c.Reserva).Include(c => c.Servicio).ToListAsync();
        }

        // GET: api/ConsumoServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoServicio>> GetConsumoServicio(int id)
        {
            var consumoServicio = await _context.ConsumoServicio.Include(c => c.Reserva).Include(c => c.Servicio).FirstOrDefaultAsync(c => c.IdConsumo == id);

            if (consumoServicio == null)
            {
                return NotFound();
            }

            return consumoServicio;
        }

        // PUT: api/ConsumoServicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumoServicio(int id, ConsumoServicio consumoServicio)
        {
            if (id != consumoServicio.IdConsumo)
            {
                return BadRequest();
            }

            _context.Entry(consumoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumoServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ConsumoServicio
        [HttpPost]
        public async Task<ActionResult<ConsumoServicio>> PostConsumoServicio(ConsumoServicio consumoServicio)
        {
            _context.ConsumoServicio.Add(consumoServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumoServicio", new { id = consumoServicio.IdConsumo }, consumoServicio);
        }

        // DELETE: api/ConsumoServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumoServicio(int id)
        {
            var consumoServicio = await _context.ConsumoServicio.FindAsync(id);
            if (consumoServicio == null)
            {
                return NotFound();
            }

            _context.ConsumoServicio.Remove(consumoServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumoServicioExists(int id)
        {
            return _context.ConsumoServicio.Any(e => e.IdConsumo == id);
        }
    }
}
