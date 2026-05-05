using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendelesekController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RendelesekController(AppDbContext context)
        {
            _context = context;
        }

        // Alapvetõ statisztikák (bevétel, darabszám, legutóbbi rendelések) lekérése
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var rendelesek = await _context.Rendelesek.ToListAsync();
                var tetelek = await _context.RendelesTetelek.ToListAsync();

                return Ok(new
                {
                    totalRevenue = rendelesek.Sum(r => r.Vegosszeg),
                    totalTickets = tetelek.Sum(t => t.Mennyiseg),
                    ordersCount = rendelesek.Count,
                    recentOrders = rendelesek.OrderByDescending(r => r.RendelesIdeje).Take(5)
                });
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpGet]
        public async Task<IActionResult> GetRendelesek()
        {
            return Ok(await _context.Rendelesek
                .OrderByDescending(r => r.RendelesIdeje)
                .Select(r => new {
                    r.Id,
                    r.FelhasznaloId,
                    r.Vegosszeg,
                    Datum = r.RendelesIdeje.ToString("yyyy. MM. dd. HH:mm"),
                    r.Statusz,
                    RendelesTetelek = r.RendelesTetelek.Select(t => new {
                        t.TermekVariansId,
                        TermekNev = t.TermekVarians != null ? t.TermekVarians.Termek.Nev : "Termék",
                        // Itt küldjük úgy, hogy a JS-nek jó legyen:
                        Varians = new
                        {
                            Meret = new
                            {
                                Megnevezes = t.TermekVarians != null ? t.TermekVarians.Meret.Megnevezes : "N/A"
                            }
                        },
                        t.Mennyiseg,
                        t.Egysegar
                    }).ToList()
                }).ToListAsync());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRendelesek(int userId)
        {
            try
            {
                var rendelesek = await _context.Rendelesek
                    .Where(r => r.FelhasznaloId == userId)
                    .OrderByDescending(r => r.RendelesIdeje)
                    .Select(r => new {
                        r.Id,
                        r.Vegosszeg,
                        Datum = r.RendelesIdeje.ToString("yyyy. MM. dd. HH:mm"),
                        r.Statusz,
                        RendelesTetelek = r.RendelesTetelek.Select(t => new {
                            t.TermekVariansId,
                            TermekNev = t.TermekVarians != null ? t.TermekVarians.Termek.Nev : "Termék",
                            Meret = t.TermekVarians != null ? t.TermekVarians.Meret.Megnevezes : "N/A",
                            t.Mennyiseg,
                            t.Egysegar
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(rendelesek);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Hiba a lekérés során!", error = ex.Message });
            }
        }

        // Új rendelés rögzítése készletkezeléssel és tranzakciókezeléssel
        [HttpPost]
        public async Task<IActionResult> PostRendeles([FromBody] RendelesPostDto dto)
        {
            if (dto == null || dto.Tetelek == null || !dto.Tetelek.Any())
            {
                return BadRequest(new { message = "Üres a rendelés vagy hiányoznak a tételek!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var rendeles = new Rendeles
                {
                    FelhasznaloId = dto.FelhasznaloId,
                    Vegosszeg = dto.Vegosszeg,
                    RendelesIdeje = DateTime.Now,
                    Statusz = "Fizetve"
                };

                _context.Rendelesek.Add(rendeles);
                await _context.SaveChangesAsync();

                foreach (var item in dto.Tetelek)
                {
                    var varians = await _context.TermekVariansok
                        .Include(v => v.Termek)
                        .FirstOrDefaultAsync(v => v.Id == item.TermekVariansId);

                    // Készlet ellenõrzése és levonása
                    if (varians == null)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { message = $"A(z) {item.TermekVariansId} ID-jú termékvariáns nem létezik!" });
                    }

                    if (varians.Keszlet < item.Mennyiseg)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { message = $"Nincs elég készlet a(z) {varians.Termek.Nev} termékbõl!" });
                    }

                    varians.Keszlet -= item.Mennyiseg;

                    var ujTetel = new RendelesTetel
                    {
                        RendelesId = rendeles.Id,
                        TermekVariansId = item.TermekVariansId,
                        Mennyiseg = item.Mennyiseg,
                        Egysegar = item.Egysegar
                    };
                    _context.RendelesTetelek.Add(ujTetel);
                }

                // Kosár ürítése a sikeres tranzakció részeként
                var userKosar = _context.Kosarak.Where(k => k.FelhasznaloId == dto.FelhasznaloId);
                _context.Kosarak.RemoveRange(userKosar);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Sikeres rendelés!", orderId = rendeles.Id });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { message = "Hiba történt a mentéskor!", error = ex.Message });
            }
        }
    }

    public class RendelesPostDto
    {
        public int FelhasznaloId { get; set; }
        public decimal Vegosszeg { get; set; }
        public List<RendelesTetelPostDto> Tetelek { get; set; } = new();
    }

    public class RendelesTetelPostDto
    {
        public int TermekVariansId { get; set; }
        public int Mennyiseg { get; set; }
        public decimal Egysegar { get; set; }
    }
}