using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreApi_PFA.Models;
using Microsoft.AspNetCore.Cors;

namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsEnabling")]
    public class SeancesController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public SeancesController(Pfa1Context context)
        {
            _context = context;
        }

        // GET: api/Seances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeance()
        {
            return await _context.Seance.ToListAsync();
        }

        // GET: api/Seances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seance.FindAsync(id);

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }

        // PUT: api/Seances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance(int id, Seance seance)
        {
            if (id != seance.Id)
            {
                return BadRequest();
            }

            _context.Entry(seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
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

        // POST: api/Seances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seance>> PostSeance([FromBody]Seance seance)
        {
            _context.Seance.Add(seance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeance", new { id = seance.Id }, seance);
        }

        // DELETE: api/Seances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seance>> DeleteSeance(int id)
        {
            var seance = await _context.Seance.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }

            _context.Seance.Remove(seance);
            await _context.SaveChangesAsync();

            return seance;
        }

        private bool SeanceExists(int id)
        {
            return _context.Seance.Any(e => e.Id == id);
        }
    }
}
