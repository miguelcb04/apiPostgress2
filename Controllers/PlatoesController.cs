using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPostgress.Modelo;

namespace ApiPostgress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoesController : ControllerBase
    {
        private readonly ApiplatosContext _context;

        public PlatoesController(ApiplatosContext context)
        {
            _context = context;
        }

        // GET: api/Platoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plato>>> GetPlatos()
        {
            return await _context.Platos.ToListAsync();
        }

        // GET: api/Platoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plato>> GetPlato(int id)
        {
            var plato = await _context.Platos.FindAsync(id);

            if (plato == null)
            {
                return NotFound();
            }

            return plato;
        }

        // PUT: api/Platoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlato(int id, Plato plato)
        {
            if (id != plato.Id)
            {
                return BadRequest();
            }

            _context.Entry(plato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatoExists(id))
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

        // POST: api/Platoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plato>> PostPlato(Plato plato)
        {
            _context.Platos.Add(plato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlato", new { id = plato.Id }, plato);
        }

        // DELETE: api/Platoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlato(int id)
        {
            var plato = await _context.Platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }

            _context.Platos.Remove(plato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatoExists(int id)
        {
            return _context.Platos.Any(e => e.Id == id);
        }
    }
}
