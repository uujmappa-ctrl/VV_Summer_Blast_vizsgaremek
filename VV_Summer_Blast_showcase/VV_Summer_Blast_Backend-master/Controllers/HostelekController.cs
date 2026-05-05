using Microsoft.AspNetCore.Mvc;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelekController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HostelekController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hostel>>> GetHostelek()
        {
            // Egyszerű lista lekérése az összes külső szálláshelyről (hostelek, diákotthonok)
            return await _context.Hostelek.ToListAsync();
        }
    }
}