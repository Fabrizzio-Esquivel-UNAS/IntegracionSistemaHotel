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
            try
            {
                return await _context.Huesped.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Huesped/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Huesped>> GetHuesped(int id)
        {
            try
            {
                var huesped = await _context.Huesped.FindAsync(id);

                if (huesped == null)
                {
                    return NotFound(new { message = "Huésped no encontrado" });
                }

                return huesped;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Huesped/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuesped(int id, Huesped huesped)
        {

            _context.Entry(huesped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HuespedExists(id))
                {
                    return NotFound(new { message = "Huésped no encontrado" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar el huésped" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Huesped
        [HttpPost]
        public async Task<ActionResult<Huesped>> PostHuesped(Huesped huesped)
        {
            try
            {
                _context.Huesped.Add(huesped);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHuesped", new { id = huesped.IdHuesped }, huesped);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Huesped/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteHuesped(int id)
        {
            try
            {
                var huesped = await _context.Huesped.FindAsync(id);
                if (huesped == null)
                {
                    return NotFound(new { message = "Huésped no encontrado" });
                }

                _context.Huesped.Remove(huesped);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool HuespedExists(int id)
        {
            return _context.Huesped.Any(e => e.IdHuesped == id);
        }
    }
}
