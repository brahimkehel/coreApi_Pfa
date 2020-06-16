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
    public class FichiersController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public FichiersController(Pfa1Context context)
        {
            _context = context;
        }

        // GET: api/Fichiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fichiers>>> GetFichiers()
        {
            return await _context.Fichiers.ToListAsync();
        }

        // GET: api/Fichiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fichiers>> GetFichiers(int id)
        {
            var fichiers = await _context.Fichiers.FindAsync(id);

            if (fichiers == null)
            {
                return NotFound();
            }

            return fichiers;
        }

        // PUT: api/Fichiers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichiers(int id, Fichiers fichiers)
        {
            if (id != fichiers.Id)
            {
                return BadRequest();
            }

            _context.Entry(fichiers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichiersExists(id))
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

        // POST: api/Fichiers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fichiers>> PostFichiers(Fichiers fichiers)
        {
            _context.Fichiers.Add(fichiers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFichiers", new { id = fichiers.Id }, fichiers);
        }

        // DELETE: api/Fichiers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fichiers>> DeleteFichiers(int id)
        {
            var fichiers = await _context.Fichiers.FindAsync(id);
            if (fichiers == null)
            {
                return NotFound();
            }

            _context.Fichiers.Remove(fichiers);
            await _context.SaveChangesAsync();

            return fichiers;
        }

        private bool FichiersExists(int id)
        {
            return _context.Fichiers.Any(e => e.Id == id);
        }
    }
}
