using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KempingekController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KempingekController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kemping>>> GetKempingek()
        {
            // A fesztivál saját kempinghelyeinek listázása
            return await _context.Kempingek.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKemping(int id)
        {
            // Egy konkrét kempinghely adatainak lekérése a részletezõ modal ablakhoz
            var kemping = await _context.Kempingek.FirstOrDefaultAsync(x => x.Id == id);

            if (kemping == null)
            {
                return NotFound(new { message = $"A(z) {id} azonosítójú kempinghely nem található." });
            }

            return Ok(kemping);
        }
    }
}