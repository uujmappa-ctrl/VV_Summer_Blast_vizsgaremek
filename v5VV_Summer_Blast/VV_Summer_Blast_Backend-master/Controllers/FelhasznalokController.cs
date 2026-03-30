using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FelhasznalokController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FelhasznalokController(AppDbContext context) => _context = context;

        // Admin lista lekérése formázott dátummal, jelszavak nélkül!
        [HttpGet]
        public async Task<IActionResult> GetFelhasznalok()
        {
            var users = await _context.Felhasznalok
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.Role,
                    CreatedAt = u.CreatedAt.ToString("yyyy. MM. dd. HH:mm")
                })
                .ToListAsync();

            return Ok(users);
        }

        // Jogosultság átkapcsolása (Admin <-> User)
        [HttpPut("{id}/role")]
        public async Task<IActionResult> ToggleRole(int id)
        {
            var user = await _context.Felhasznalok.FindAsync(id);
            if (user == null) return NotFound(new { message = "Felhasználó nem található!" });

            user.Role = user.Role == "Admin" ? "User" : "Admin";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Jogosultság sikeresen frissítve!", newRole = user.Role });
        }
    }
}