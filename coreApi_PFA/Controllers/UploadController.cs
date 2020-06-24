using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using coreApi_PFA.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreApi_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsEnabling")]
    public class UploadController : ControllerBase
    {
        private readonly Pfa1Context _context;

        public UploadController(Pfa1Context context)
        {
            _context = context;
        }

        [HttpPost("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var user = _context.Utilisateur.FromSqlInterpolated($"SELECT * FROM dbo.Utilisateur").Where(res => res.Id == id).FirstOrDefault();
                    var fichier = new Fichiers();
                    fichier.Nom = user.Nom;
                    fichier.Status = user.Status;
                    fichier.NomFichier = fileName;
                    _context.Fichiers.Add(fichier);
                    await _context.SaveChangesAsync();
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("{id}")]
        [Route("Download/{id}")]
        public async Task<ActionResult> Download(int id)
        {
            var f= _context.Fichiers.FromSqlInterpolated($"SELECT * FROM dbo.Fichiers").Where(res => res.Id == id).FirstOrDefault();
            var path = @"C:/Users/MICRO/source/repos/coreApi_PFA/coreApi_PFA/Resources/Images/"+f.NomFichier;
            var memory = new MemoryStream();
            using(var stream=new FileStream(path,FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext],Path.GetFileName(path));
        }
        private Dictionary<string,string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt","text/plain" },
                {".pdf","application/pdf" },
                {".doc","application/vnd.ms-word" },
                {".docx","application/vnd.ms-word" },
                {".xls","application/vnd.ms-excel" },
                { ".png","image/png" },
                {".jpg","image/jpg" },
                {".jpeg","image/jpeg" },
            };
        }
    }
}