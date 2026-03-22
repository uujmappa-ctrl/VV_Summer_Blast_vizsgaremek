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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest(new { message = "Email és jelszó kitöltése kötelezõ!" });

            // JAVÍTÁS: u.PasswordHash (modell) == dto.Password (dto)
            var user = await _context.Felhasznalok
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Hibás e-mail cím vagy jelszó!" });

            return Ok(new
            {
                id = user.Id,
                name = user.UserName,
                role = user.Role,
                email = user.Email
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Felhasznalok.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { message = "Ez az e-mail cím már foglalt!" });

            var ujUser = new Felhasznalo
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = dto.Password, // Egyszerûsített v5 szint: sima szövegként mentjük
                Role = "User",
                CreatedAt = DateTime.Now
            };

            _context.Felhasznalok.Add(ujUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sikeres regisztráció!" });
        }
    }
}