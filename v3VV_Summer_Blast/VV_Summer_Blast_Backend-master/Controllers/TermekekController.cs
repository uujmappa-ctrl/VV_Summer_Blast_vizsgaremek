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
            return Ok(await _context.Termekek.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTermek(int id)
        {
            var termek = await _context.Termekek.FindAsync(id);
            if (termek == null) return NotFound();
            return Ok(termek);
        }
    }
}