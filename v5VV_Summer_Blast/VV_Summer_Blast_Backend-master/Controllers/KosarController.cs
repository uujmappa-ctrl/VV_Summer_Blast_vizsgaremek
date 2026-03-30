using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    public class KosarBejovoDto
    {
        public int FelhasznaloId { get; set; }
        public int TermekId { get; set; }
        public int Mennyiseg { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class KosarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KosarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Kosar>>> GetUserKosar(int userId)
        {
            return await _context.Kosarak
                .Include(k => k.Termek)
                .Where(k => k.FelhasznaloId == userId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddToKosar([FromBody] KosarBejovoDto dto)
        {
            var letezoElem = await _context.Kosarak
                .FirstOrDefaultAsync(k => k.FelhasznaloId == dto.FelhasznaloId && k.TermekId == dto.TermekId);

            if (letezoElem != null)
            {
                letezoElem.Mennyiseg += dto.Mennyiseg;
            }
            else
            {
                var ujKosarElem = new Kosar
                {
                    FelhasznaloId = dto.FelhasznaloId,
                    TermekId = dto.TermekId,
                    Mennyiseg = dto.Mennyiseg
                };
                _context.Kosarak.Add(ujKosarElem);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Sikeresen a kosárhoz adva!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromKosar(int id)
        {
            var kosarElem = await _context.Kosarak.FindAsync(id);
            if (kosarElem == null) return NotFound();

            _context.Kosarak.Remove(kosarElem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}