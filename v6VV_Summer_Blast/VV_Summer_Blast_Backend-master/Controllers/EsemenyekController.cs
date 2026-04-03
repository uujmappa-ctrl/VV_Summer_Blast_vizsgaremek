using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsemenyekController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EsemenyekController(AppDbContext context) => _context = context;

        // A teljes fesztiválprogram lekérése időrendben
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Esemeny>>> GetProgram([FromQuery] string? mufaj = null)
        {
            var query = _context.Esemenyek
                .Include(e => e.Eloado)
                .Include(e => e.Szinpad)
                .AsQueryable();

            // Itt is szűrhetünk műfajra a programtáblában
            if (!string.IsNullOrWhiteSpace(mufaj))
            {
                query = query.Where(e => e.Eloado != null && e.Eloado.Mufaj == mufaj);
            }

            // A kezdési időpont a legfontosabb a látogatóknak
            return await query.OrderBy(e => e.Kezdes).ToListAsync();
        }
    }
}