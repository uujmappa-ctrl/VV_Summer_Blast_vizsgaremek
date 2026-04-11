using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    // DTO a kosárba tételhez
    public class KosarBejovoDto
    {
        public int FelhasznaloId { get; set; }
        public int TermekVariansId { get; set; }
        public int Mennyiseg { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class KosarController : ControllerBase
    {
        private readonly AppDbContext _context;
        public KosarController(AppDbContext context) => _context = context;

        // Felhasználó kosarának lekérése a kapcsolódó adatokkal (Termék név, méret)
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Kosar>>> GetUserKosar(int userId)
        {
            return await _context.Kosarak
                .Include(k => k.TermekVarians)
                    .ThenInclude(v => v.Termek)
                .Include(k => k.TermekVarians)
                    .ThenInclude(v => v.Meret)
                .Where(k => k.FelhasznaloId == userId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddToKosar([FromBody] KosarBejovoDto dto)
        {
            if (dto == null || dto.FelhasznaloId <= 0 || dto.TermekVariansId <= 0)
            {
                return BadRequest(new { message = "Hiányzó vagy hibás adatok (ID-k)." });
            }

            // Megnézzük, van-e már ilyen termék a kosárban
            var letezoElem = await _context.Kosarak
                .FirstOrDefaultAsync(k => k.FelhasznaloId == dto.FelhasznaloId
                                       && k.TermekVariansId == dto.TermekVariansId);

            if (letezoElem != null)
            {
                // Ha már benne van, csak a mennyiséget növeljük
                letezoElem.Mennyiseg += dto.Mennyiseg;
                _context.Kosarak.Update(letezoElem);
            }
            else
            {
                // Új tétel létrehozása
                var ujKosarElem = new Kosar
                {
                    FelhasznaloId = dto.FelhasznaloId,
                    TermekVariansId = dto.TermekVariansId,
                    Mennyiseg = dto.Mennyiseg
                };
                _context.Kosarak.Add(ujKosarElem);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Sikeresen a kosárhoz adva!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Adatbázis hiba!", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromKosar(int id)
        {
            var kosarElem = await _context.Kosarak.FindAsync(id);
            if (kosarElem == null) return NotFound(new { message = "A tétel nem található." });

            _context.Kosarak.Remove(kosarElem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tétel törölve." });
        }
    }
}