using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreApi_PFA.Models;
using Microsoft.AspNetCore.JsonPatch;
namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrateursController : ControllerBase
    {
        private readonly Pfa1Context _context;


        public AdministrateursController(Pfa1Context context)
        {
            _context = context;
        }
        // GET: api/Administrateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrateur>>> GetAdministrateur()
        {
            return await _context.Administrateur.ToListAsync();
        }

        // GET: api/Administrateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrateur>> GetAdministrateur(int id)
        {
            var administrateur = await _context.Administrateur.FindAsync(id);

            if (administrateur == null)
            {
                return NotFound();
            }

            return administrateur;
        }

        // PUT: api/Administrateurs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrateur(int id, Administrateur administrateur)
        {
            if (id != administrateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(administrateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrateurExists(id))
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

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Administrateur> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var authorFromDB = await _context.Administrateur.FirstOrDefaultAsync(x => x.Id == id);
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

        // POST: api/Administrateurs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Administrateur>> PostAdministrateur(Administrateur administrateur)
        {
            _context.Administrateur.Add(administrateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrateur", new { id = administrateur.Id }, administrateur);
        }

        // DELETE: api/Administrateurs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrateur>> DeleteAdministrateur(int id)
        {
            var administrateur = await _context.Administrateur.FindAsync(id);
            if (administrateur == null)
            {
                return NotFound();
            }

            _context.Administrateur.Remove(administrateur);
            await _context.SaveChangesAsync();

            return administrateur;
        }

        private bool AdministrateurExists(int id)
        {
            return _context.Administrateur.Any(e => e.Id == id);
        }
    }
}
