using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermekekController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TermekekController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTermekek()
        {
            var termekek = await _context.Termekek
                .Include(t => t.Variansok)
                    .ThenInclude(v => v.Meret)
                .ToListAsync();

            return Ok(termekek);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTermek(int id)
        {
            var termek = await _context.Termekek
                .Include(t => t.Variansok)
                    .ThenInclude(v => v.Meret)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (termek == null) return NotFound();
            return Ok(termek);
        }

        // Termék adatainak és készletének módosítása
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermek(int id, Termek termek)
        {
            if (id != termek.Id) return BadRequest();

            var existingTermek = await _context.Termekek
                .Include(t => t.Variansok)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTermek == null) return NotFound();

            // Alapadatok frissítése
            existingTermek.Nev = termek.Nev;
            existingTermek.Ar = termek.Ar;
            existingTermek.Tipus = termek.Tipus;
            existingTermek.KepUrl = termek.KepUrl;

            // Készletinformációk frissítése variánsonként
            if (termek.Variansok != null)
            {
                foreach (var varians in termek.Variansok)
                {
                    var existingVarians = existingTermek.Variansok
                        .FirstOrDefault(v => v.Id == varians.Id);

                    if (existingVarians != null)
                    {
                        existingVarians.Keszlet = varians.Keszlet;
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Termekek.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return Ok(new { message = "Sikeresen frissítve az adatbázisban!" });
        }
    }
}