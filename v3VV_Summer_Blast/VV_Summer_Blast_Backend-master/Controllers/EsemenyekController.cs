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

        // A teljes fesztiválprogram lekérése időrendben, szűrés nélkül
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Esemeny>>> GetProgram()
        {
            return await _context.Esemenyek
                .Include(e => e.Eloado)
                .Include(e => e.Szinpad)
                .OrderBy(e => e.Kezdes)
                .ToListAsync();
        }
    }
}