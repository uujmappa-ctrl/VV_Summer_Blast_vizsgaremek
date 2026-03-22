using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;
using BCrypt.Net;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Biztonsági korlátozás: ne lehessen külsõleg admin emailt regisztrálni
            if (dto.Email.ToLower() == "vvadmin@gmail.com")
            {
                return BadRequest(new { message = "Ezt az email címet nem használhatod regisztrációhoz!" });
            }

            // Ellenõrizzük, hogy létezik-e már a felhasználó
            if (await _context.Felhasznalok.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(new { message = "Ez az email cím már regisztrálva van!" });
            }

            // Jelszó titkosítása BCrypt-tel a biztonságos tároláshoz
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new Felhasznalo
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            _context.Felhasznalok.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sikeres regisztráció!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Speciális eset: Mester Admin kezelése (ha még nincs a DB-ben, létrehozzuk)
            if (dto.Email == "vvAdmin@gmail.com" && dto.Password == "vvAdmin123")
            {
                var adminUser = await _context.Felhasznalok.FirstOrDefaultAsync(u => u.Email == "vvAdmin@gmail.com");

                if (adminUser == null)
                {
                    adminUser = new Felhasznalo
                    {
                        UserName = "vvAdmin",
                        Email = "vvAdmin@gmail.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("vvAdmin123"),
                        Role = "Admin",
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Felhasznalok.Add(adminUser);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Sikeres bejelentkezés (Mester Admin)!", userId = adminUser.Id, userName = adminUser.UserName, role = adminUser.Role });
            }

            // Sima felhasználó keresése
            var user = await _context.Felhasznalok.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Hibás email vagy jelszó!" });
            }

            // Hash összehasonlítása a megadott jelszóval
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Hibás email vagy jelszó!" });
            }

            return Ok(new { message = "Sikeres bejelentkezés!", userId = user.Id, userName = user.UserName, role = user.Role });
        }
    }
}