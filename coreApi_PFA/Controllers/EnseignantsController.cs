using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreApi_PFA.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;

namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsEnabling")]
    public class EnseignantsController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public EnseignantsController(Pfa1Context context)
        {
            _context = context;
        }

        //GET: api/Enseignants/Nb
        [HttpGet]
        [Route("Nb")]
        public async Task<int> GetNb()
        {
            return await _context.Enseignant.CountAsync();
        }

        // GET: api/Enseignants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetEnseignant()
        {
            return await _context.Enseignant.ToListAsync();
        }

        // GET: api/Enseignants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enseignant>> GetEnseignant(int id)
        {
            var enseignant = await _context.Enseignant.FindAsync(id);

            if (enseignant == null)
            {
                return NotFound();
            }

            return enseignant;
        }

        // PUT: api/Enseignants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Route("Modifier")]
        public async Task<IActionResult> PutEnseignant(int id, Enseignant enseignant)
        {
            if (id != enseignant.Id)
            {
                return BadRequest();
            }

            _context.Entry(enseignant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnseignantExists(id))
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

        // POST: api/Enseignants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("Ajouter")]
        public async Task<ActionResult<Enseignant>> PostEnseignant(Enseignant enseignant)
        {
            _context.Enseignant.Add(enseignant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnseignant", new { id = enseignant.Id }, enseignant);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Enseignant> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var authorFromDB = await _context.Enseignant.FirstOrDefaultAsync(x => x.Id == id);

            if (authorFromDB == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(authorFromDB);

            var isValid = TryValidateModel(authorFromDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Enseignants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enseignant>> DeleteEnseignant(int id)
        {
            var enseignant = await _context.Enseignant.FindAsync(id);
            if (enseignant == null)
            {
                return NotFound();
            }

            _context.Enseignant.Remove(enseignant);
            await _context.SaveChangesAsync();

            return enseignant;
        }

        private bool EnseignantExists(int id)
        {
            return _context.Enseignant.Any(e => e.Id == id);
        }
    }
}
