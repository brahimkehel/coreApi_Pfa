using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreApi_PFA.Models;
using SQLitePCL;
using Microsoft.AspNetCore.Cors;

namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsEnabling")]
    public class AffectationsController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public AffectationsController(Pfa1Context context)
        {
            _context = context;
        }

        // GET: api/Affectations
        [HttpGet]
        public async Task<object> GetAffectation()
        {
            return await (from a in _context.Affectation
                          join e in _context.Enseignant on a.IdEns equals e.Id
                          join f in _context.Filiere on a.IdFiliere equals f.Id
                          join m in _context.Matiere on a.IdMatiere equals m.Id
                          select new
                          {
                              IdFiliere =a.IdFiliere,
                              Libelle_Filiere =f.Libelle,
                              IdMatiere=a.IdMatiere,
                              Libelle_Matiere=m.Libelle,
                              IdEns=a.IdEns,
                              Libelle_Enseignant=e.Nom
                          }).ToListAsync();
        }

        // GET: api/Affectations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Affectation>> GetAffectation(int id)
        {
            var affectation = await _context.Affectation.FindAsync(id);

            if (affectation == null)
            {
                return NotFound();
            }

            return affectation;
        }

        // PUT: api/Affectations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAffectation(int id, Affectation affectation)
        {
            if (id != affectation.IdFiliere)
            {
                return BadRequest();
            }

            _context.Entry(affectation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AffectationExists(id))
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

        // POST: api/Affectations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Affectation>> PostAffectation(Affectation affectation)
        {
            _context.Affectation.Add(affectation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AffectationExists(affectation.IdFiliere))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAffectation", new { id = affectation.IdFiliere }, affectation);
        }

        // DELETE: api/Affectations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Affectation>> DeleteAffectation(int id)
        {
            var affectation = await _context.Affectation.FindAsync(id);
            if (affectation == null)
            {
                return NotFound();
            }

            _context.Affectation.Remove(affectation);
            await _context.SaveChangesAsync();

            return affectation;
        }

        private bool AffectationExists(int id)
        {
            return _context.Affectation.Any(e => e.IdFiliere == id);
        }
    }
}
