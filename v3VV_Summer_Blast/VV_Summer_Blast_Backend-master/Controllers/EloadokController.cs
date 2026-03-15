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

        // Összes előadó lekérése
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eloado>>> GetEloadok()
        {
            return await _context.Eloadok.ToListAsync();
        }

        // Egy előadó lekérése azonosító alapján
        [HttpGet("{id}")]
        public async Task<ActionResult<Eloado>> GetEloado(int id)
        {
            var eloado = await _context.Eloadok.FindAsync(id);

            if (eloado == null) return NotFound();
            return eloado;
        }

        
    }
}