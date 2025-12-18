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
    public class CategoriaHabitacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaHabitacionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaHabitacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaHabitacion>>> GetCategoriaHabitaciones()
        {
            try
            {
                return await _context.CategoriaHabitacion.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/CategoriaHabitacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaHabitacion>> GetCategoriaHabitacion(int id)
        {
            try
            {
                var categoriaHabitacion = await _context.CategoriaHabitacion.FindAsync(id);

                if (categoriaHabitacion == null)
                {
                    return NotFound(new { message = "Categoría de habitación no encontrada" });
                }

                return categoriaHabitacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/CategoriaHabitacion/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutCategoriaHabitacion(int id, CategoriaHabitacion categoriaHabitacion)
        {

            _context.Entry(categoriaHabitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaHabitacionExists(id))
                {
                    return NotFound(new { message = "Categoría de habitación no encontrada" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar la categoría" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/CategoriaHabitacion
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoriaHabitacion>> PostCategoriaHabitacion(CategoriaHabitacion categoriaHabitacion)
        {
            try
            {
                _context.CategoriaHabitacion.Add(categoriaHabitacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategoriaHabitacion", new { id = categoriaHabitacion.IdCategoria }, categoriaHabitacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/CategoriaHabitacion/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCategoriaHabitacion(int id)
        {
            try
            {
                var categoriaHabitacion = await _context.CategoriaHabitacion.FindAsync(id);
                if (categoriaHabitacion == null)
                {
                    return NotFound(new { message = "Categoría de habitación no encontrada" });
                }

                _context.CategoriaHabitacion.Remove(categoriaHabitacion);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool CategoriaHabitacionExists(int id)
        {
            return _context.CategoriaHabitacion.Any(e => e.IdCategoria == id);
        }
    }
}
