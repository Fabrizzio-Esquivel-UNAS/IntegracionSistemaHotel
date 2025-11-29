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
    public class HuespedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HuespedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Huesped
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Huesped>>> GetHuespedes()
        {
            return await _context.Huesped.ToListAsync();
        }

        // GET: api/Huesped/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Huesped>> GetHuesped(int id)
        {
            var huesped = await _context.Huesped.FindAsync(id);

            if (huesped == null)
            {
                return NotFound();
            }

            return huesped;
        }

        // PUT: api/Huesped/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuesped(int id, Huesped huesped)
        {
            if (id != huesped.IdHuesped)
            {
                return BadRequest();
            }

            _context.Entry(huesped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HuespedExists(id))
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

        // POST: api/Huesped
        [HttpPost]
        public async Task<ActionResult<Huesped>> PostHuesped(Huesped huesped)
        {
            _context.Huesped.Add(huesped);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHuesped", new { id = huesped.IdHuesped }, huesped);
        }

        // DELETE: api/Huesped/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteHuesped(int id)
        {
            var huesped = await _context.Huesped.FindAsync(id);
            if (huesped == null)
            {
                return NotFound();
            }

            _context.Huesped.Remove(huesped);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HuespedExists(int id)
        {
            return _context.Huesped.Any(e => e.IdHuesped == id);
        }
    }
}
