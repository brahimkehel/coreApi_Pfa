using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreApi_PFA.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.VisualBasic;

namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsEnabling")]
    public class EtudiantsController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public EtudiantsController(Pfa1Context context)
        {
            _context = context;
        }
        //GET: api/Etudiants/Nb
        [HttpGet]
        [Route("Nb")]
        public async Task<int> GetNb()
        {
            return await _context.Etudiant.CountAsync();
        }

        

        // GET: api/Etudiants
        [HttpGet]
        public async Task<object> GetEtudiant()
        {
            return await (from e in _context.Etudiant join f in _context.Filiere on e.IdFiliere equals f.Id
                     select new
                     {   Id = e.Id,
                         Cin = e.Cin,
                         DateNais = e.DateNais,
                         Nom = e.Nom,
                         Prenom = e.Prenom,
                         Email = e.Email,
                         MotdePasse = e.MotdePasse,
                         Adresse = e.Adresse,
                         Telephone = e.Telephone,
                         Genre=e.Genre,
                         CNE = e.Cne,
                         IdFiliere = e.IdFiliere,
                         Filiere = f.Libelle,
                     }).ToListAsync();
        }

        // GET: api/Etudiants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Etudiant>> GetEtudiant(int id)
        {
            var etudiant = await _context.Etudiant.FindAsync(id);

            if (etudiant == null)
            {
                return NotFound();
            }

            return etudiant;
        }

        // PUT: api/Etudiants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtudiant(int id, Etudiant etudiant)
        {
            if (id != etudiant.Id)
            {
                return BadRequest();
            }

            _context.Entry(etudiant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(id))
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

        // POST: api/Etudiants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("Ajouter")]
        public async Task<ActionResult<Etudiant>> PostEtudiant(Etudiant etudiant)
        {
            _context.Etudiant.Add(etudiant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtudiant", new { id = etudiant.Id }, etudiant);
        }

        // DELETE: api/Etudiants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Etudiant>> DeleteEtudiant(int id)
        {
            var etudiant = await _context.Etudiant.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }

            _context.Etudiant.Remove(etudiant);
            await _context.SaveChangesAsync();

            return etudiant;
        }

        private bool EtudiantExists(int id)
        {
            return _context.Etudiant.Any(e => e.Id == id);
        }
    }
}
