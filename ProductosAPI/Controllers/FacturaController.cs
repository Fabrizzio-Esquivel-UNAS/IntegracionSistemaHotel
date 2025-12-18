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
    public class FacturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Factura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            try
            {
                return await _context.Factura.Include(f => f.Reserva).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Factura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            try
            {
                var factura = await _context.Factura.Include(f => f.Reserva).FirstOrDefaultAsync(f => f.IdFactura == id);

                if (factura == null)
                {
                    return NotFound(new { message = "Factura no encontrada" });
                }

                return factura;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Factura/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound(new { message = "Factura no encontrada" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error de concurrencia al actualizar la factura" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Factura
        [HttpPost]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            try
            {
                _context.Factura.Add(factura);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFactura", new { id = factura.IdFactura }, factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Factura/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var factura = await _context.Factura.FindAsync(id);
                if (factura == null)
                {
                    return NotFound(new { message = "Factura no encontrada" });
                }

                _context.Factura.Remove(factura);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.IdFactura == id);
        }
    }
}
