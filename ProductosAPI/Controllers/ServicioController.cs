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
    public class ServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            try
            {
                return await _context.Servicio.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            try
            {
                var servicio = await _context.Servicio.FindAsync(id);

                if (servicio == null)
                {
                    return NotFound(new { message = "Servicio no encontrado" });
                }

                return servicio;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Servicio/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
                {
                    return NotFound(new { message = "Servicio no encontrado" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar el servicio" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Servicio
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            try
            {
                _context.Servicio.Add(servicio);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetServicio", new { id = servicio.IdServicio }, servicio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            try
            {
                var servicio = await _context.Servicio.FindAsync(id);
                if (servicio == null)
                {
                    return NotFound(new { message = "Servicio no encontrado" });
                }

                _context.Servicio.Remove(servicio);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.IdServicio == id);
        }
    }
}
