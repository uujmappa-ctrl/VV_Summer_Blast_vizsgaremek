using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EloadokController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EloadokController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eloado>>> GetEloadok([FromQuery] string? mufaj = null)
        {
            // Alap lekérdezés, amibe beágyazzuk az eseményeket is
            var query = _context.Eloadok.Include(e => e.Esemenyek).AsQueryable();

            // Opcionális szűrés műfaj alapján (pl. Rock, Pop)
            if (!string.IsNullOrWhiteSpace(mufaj))
            {
                query = query.Where(e => e.Mufaj == mufaj);
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Eloado>> GetEloado(int id)
        {
            var eloado = await _context.Eloadok
                .Include(e => e.Esemenyek)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eloado == null) return NotFound();
            return eloado;
        }

        // Admin funkció: Új fellépő rögzítése
        [HttpPost]
        public async Task<ActionResult<Eloado>> PostEloado(Eloado eloado)
        {
            _context.Eloadok.Add(eloado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEloado), new { id = eloado.Id }, eloado);
        }

        // Admin funkció: Előadó adatainak módosítása
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEloado(int id, Eloado eloado)
        {
            if (id != eloado.Id) return BadRequest("Az ID-k nem egyeznek!");

            _context.Entry(eloado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Eloadok.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEloado(int id)
        {
            var eloado = await _context.Eloadok.FindAsync(id);
            if (eloado == null) return NotFound();

            _context.Eloadok.Remove(eloado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}