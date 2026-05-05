using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Controllers;
using VVSummerBlastBackendAPI.Data;
using VVSummerBlastBackendAPI.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VVSummerBlast.Tests
{
    public class ControllerTests
    {
        // Segédmetódus a tiszta adatbázishoz minden teszthez
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new AppDbContext(options);
        }

        #region AUTH CONTROLLER TESZTEK

        [Fact]
        public async Task Register_AdminEmaillel_BadRequestetAd()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new AuthController(context);
            var dto = new RegisterDto { Email = "vvadmin@gmail.com", Password = "123", UserName = "Hacker" };

            // Act
            var result = await controller.Register(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_MesterAdmin_AutomatikusanLetrehozzaEsBelepteti()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new AuthController(context);
            var dto = new LoginDto { Email = "vvAdmin@gmail.com", Password = "vvAdmin123" };

            // Act
            var result = await controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Mester Admin", okResult.Value.ToString());
            Assert.Equal(1, context.Felhasznalok.Count());
        }

        #endregion

        #region ELŐADÓK CONTROLLER TESZTEK

        [Fact]
        public async Task GetEloadok_MufajSzures_CsakAMegfelelotAdjaVissza()
        {
            // Arrange
            using var context = GetDbContext();
            context.Eloadok.AddRange(new List<Eloado> {
                new Eloado { Nev = "Rock Band", Mufaj = "Rock" },
                new Eloado { Nev = "Pop Star", Mufaj = "Pop" }
            });
            await context.SaveChangesAsync();
            var controller = new EloadokController(context);

            // Act
            var result = await controller.GetEloadok("Rock");

            // Assert
            var list = Assert.IsType<List<Eloado>>(result.Value);
            Assert.Single(list);
            Assert.Equal("Rock Band", list[0].Nev);
        }

        [Fact]
        public async Task PostEloado_SikeresenMentEsIdtGeneral()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new EloadokController(context);
            var ujEloado = new Eloado { Nev = "Új Tehetség", Mufaj = "Jazz" };

            // Act
            var result = await controller.PostEloado(ujEloado);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var mentett = Assert.IsType<Eloado>(createdResult.Value);
            Assert.True(mentett.Id > 0);
        }

        [Fact]
        public async Task DeleteEloado_LetezoIdre_NoContentetAd()
        {
            // Arrange
            using var context = GetDbContext();
            var eloado = new Eloado { Nev = "Törlendő" };
            context.Eloadok.Add(eloado);
            await context.SaveChangesAsync();
            var controller = new EloadokController(context);

            // Act
            var result = await controller.DeleteEloado(eloado.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Eloadok);
        }

        #endregion

        #region ESEMÉNYEK CONTROLLER TESZTEK

        [Fact]
        public async Task GetProgram_RendeziIdopontSzerint()
        {
            // Arrange
            using var context = GetDbContext();

            var eloado = new Eloado { Nev = "Teszt Előadó", Mufaj = "Rock" };
            var szinpad = new Szinpad { Nev = "Nagyszínpad" };
            context.Eloadok.Add(eloado);
            context.Szinpadok.Add(szinpad);
            await context.SaveChangesAsync();

            var kesobbi = new Esemeny
            {
                EloadoId = eloado.Id,
                SzinpadId = szinpad.Id,
                Kezdes = DateTime.Now.AddHours(5),
                Vege = DateTime.Now.AddHours(6),
                Leiras = "Esti koncert"
            };
            var korabbi = new Esemeny
            {
                EloadoId = eloado.Id,
                SzinpadId = szinpad.Id,
                Kezdes = DateTime.Now.AddHours(1),
                Vege = DateTime.Now.AddHours(2),
                Leiras = "Délutáni koncert"
            };

            context.Esemenyek.AddRange(kesobbi, korabbi);
            await context.SaveChangesAsync();

            var controller = new EsemenyekController(context);

            // Act
            var result = await controller.GetProgram(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Esemeny>>>(result);
            var list = Assert.IsType<List<Esemeny>>(actionResult.Value);

            Assert.Equal(korabbi.Kezdes, list[0].Kezdes);
            Assert.Equal("Délutáni koncert", list[0].Leiras);
        }

        [Fact]
        public async Task GetProgram_MufajSzures_CsakAMegfeleloEloadotAdjaVissza()
        {
            // Arrange
            using var context = GetDbContext();

            var rockEloado = new Eloado { Nev = "Rock Király", Mufaj = "Rock" };
            var popEloado = new Eloado { Nev = "Pop Herceg", Mufaj = "Pop" };
            var szinpad = new Szinpad { Nev = "A Szinpad" };

            context.Eloadok.AddRange(rockEloado, popEloado);
            context.Szinpadok.Add(szinpad);
            await context.SaveChangesAsync();

            context.Esemenyek.Add(new Esemeny { EloadoId = rockEloado.Id, SzinpadId = szinpad.Id, Leiras = "Rock Buli", Kezdes = DateTime.Now });
            context.Esemenyek.Add(new Esemeny { EloadoId = popEloado.Id, SzinpadId = szinpad.Id, Leiras = "Pop Buli", Kezdes = DateTime.Now.AddHours(1) });
            await context.SaveChangesAsync();

            var controller = new EsemenyekController(context);

            // Act
            var result = await controller.GetProgram("Rock");

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Esemeny>>>(result);
            var list = Assert.IsType<List<Esemeny>>(actionResult.Value);

            Assert.Single(list);
            Assert.Equal("Rock Király", list[0].Eloado.Nev);
            Assert.Equal("Rock Buli", list[0].Leiras);
        }

        #endregion

        #region TERMÉKEK CONTROLLER TESZTEK

        [Fact]
        public async Task GetTermekek_MindenAdatotEsVariansBetolt()
        {
            // Arrange
            using var context = GetDbContext();
            var meret = new Meret { Megnevezes = "XL" };
            context.Meretek.Add(meret);

            var termek = new Termek { Nev = "Fesztivál Póló", Ar = 8500, Tipus = "Ruházat" };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            context.TermekVariansok.Add(new TermekVarians { TermekId = termek.Id, MeretId = meret.Id, Keszlet = 50 });
            await context.SaveChangesAsync();

            var controller = new TermekekController(context);

            // Act
            var result = await controller.GetTermekek();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var termekek = Assert.IsType<List<Termek>>(okResult.Value);

            Assert.NotEmpty(termekek);
            Assert.Equal("Fesztivál Póló", termekek[0].Nev);
            Assert.Equal("XL", termekek[0].Variansok.First().Meret.Megnevezes);
        }

        [Fact]
        public async Task GetTermek_LetezoIdre_MegfeleloTermeketAd()
        {
            // Arrange
            using var context = GetDbContext();
            var termek = new Termek
            {
                Nev = "Egyedi Termék",
                Ar = 1000,
                Tipus = "Teszt"
            };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var controller = new TermekekController(context);

            // Act
            var result = await controller.GetTermek(termek.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var visszakapott = Assert.IsType<Termek>(okResult.Value);
            Assert.Equal(termek.Id, visszakapott.Id);
        }

        [Fact]
        public async Task GetTermek_NemLetezoIdre_NotFoundotAd()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new TermekekController(context);

            // Act
            var result = await controller.GetTermek(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutTermek_KeszletFrissites_Sikeres()
        {
            // Arrange
            using var context = GetDbContext();
            var termek = new Termek
            {
                Nev = "Teszt Termék",
                Ar = 500,
                Tipus = "Merch"
            };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var varians = new TermekVarians { TermekId = termek.Id, Keszlet = 10 };
            context.TermekVariansok.Add(varians);
            await context.SaveChangesAsync();

            var controller = new TermekekController(context);

            var modositottTermek = new Termek
            {
                Id = termek.Id,
                Nev = "Teszt Termék",
                Ar = 500,
                Tipus = "Merch",
                Variansok = new List<TermekVarians> {
                    new TermekVarians { Id = varians.Id, Keszlet = 25 }
                }
            };

            // Act
            var result = await controller.PutTermek(termek.Id, modositottTermek);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var frissitettVarians = await context.TermekVariansok.FindAsync(varians.Id);
            Assert.Equal(25, frissitettVarians.Keszlet);
        }

        [Fact]
        public async Task PutTermek_HibasId_BadRequestetAd()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new TermekekController(context);
            var termek = new Termek { Id = 1, Nev = "Hibás" };

            // Act
            var result = await controller.PutTermek(2, termek);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region KOSÁR CONTROLLER TESZTEK

        [Fact]
        public async Task GetUserKosar_VisszaadjaAFelhasznaloTeteleit()
        {
            // Arrange
            using var context = GetDbContext();

            var felhasznalo = new Felhasznalo
            {
                Email = "teszt@gmail.com",
                UserName = "TesztElek",
                PasswordHash = "hashed_password"
            };
            context.Felhasznalok.Add(felhasznalo);
            await context.SaveChangesAsync();

            var meret = new Meret { Megnevezes = "L" };
            context.Meretek.Add(meret);

            var termek = new Termek { Nev = "Fesztivál Póló", Ar = 5000, Tipus = "Ruházat" };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var varians = new TermekVarians
            {
                TermekId = termek.Id,
                MeretId = meret.Id,
                Keszlet = 10
            };
            context.TermekVariansok.Add(varians);
            await context.SaveChangesAsync();

            var kosarElem = new Kosar
            {
                FelhasznaloId = felhasznalo.Id,
                TermekVariansId = varians.Id,
                Mennyiseg = 2
            };
            context.Kosarak.Add(kosarElem);
            await context.SaveChangesAsync();

            var controller = new KosarController(context);

            // Act
            var result = await controller.GetUserKosar(felhasznalo.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Kosar>>>(result);
            var lista = Assert.IsAssignableFrom<IEnumerable<Kosar>>(actionResult.Value);

            Assert.NotEmpty(lista);
            Assert.Equal(2, lista.First().Mennyiseg);
            Assert.Equal("Fesztivál Póló", lista.First().TermekVarians.Termek.Nev);
            Assert.Equal("L", lista.First().TermekVarians.Meret.Megnevezes);
        }

        [Fact]
        public async Task AddToKosar_UjTermeknel_UjSortHozLeltre()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new KosarController(context);
            var dto = new KosarBejovoDto { FelhasznaloId = 1, TermekVariansId = 1, Mennyiseg = 1 };

            // Act
            var result = await controller.AddToKosar(dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, await context.Kosarak.CountAsync());
        }

        [Fact]
        public async Task AddToKosar_LetezoTermeknel_MennyisegetNovel()
        {
            // Arrange
            using var context = GetDbContext();
            context.Kosarak.Add(new Kosar { FelhasznaloId = 1, TermekVariansId = 1, Mennyiseg = 1 });
            await context.SaveChangesAsync();

            var controller = new KosarController(context);
            var dto = new KosarBejovoDto { FelhasznaloId = 1, TermekVariansId = 1, Mennyiseg = 1 };

            // Act
            await controller.AddToKosar(dto);

            // Assert
            var kosarElem = await context.Kosarak.FirstAsync();
            Assert.Equal(2, kosarElem.Mennyiseg);
            Assert.Equal(1, await context.Kosarak.CountAsync());
        }

        [Fact]
        public async Task AddToKosar_HibasAdatokkal_BadRequestetAd()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new KosarController(context);
            var hibasDto = new KosarBejovoDto { FelhasznaloId = 0, TermekVariansId = 1, Mennyiseg = 1 };

            // Act
            var result = await controller.AddToKosar(hibasDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFromKosar_LetezoIdre_TorliATetelt()
        {
            // Arrange
            using var context = GetDbContext();
            var elem = new Kosar { FelhasznaloId = 1, TermekVariansId = 1, Mennyiseg = 5 };
            context.Kosarak.Add(elem);
            await context.SaveChangesAsync();

            var controller = new KosarController(context);

            // Act
            await controller.DeleteFromKosar(elem.Id);

            // Assert
            Assert.Empty(context.Kosarak);
        }

        #endregion

        #region RENDELÉSEK CONTROLLER TESZTEK

        [Fact]
        public async Task GetStats_VisszaadjaAStatisztikakat()
        {
            // Arrange
            using var context = GetDbContext();
            context.Rendelesek.Add(new Rendeles { Vegosszeg = 10000, RendelesIdeje = DateTime.Now, Statusz = "Fizetve", FelhasznaloId = 1 });
            context.RendelesTetelek.Add(new RendelesTetel { Mennyiseg = 2, Egysegar = 5000, RendelesId = 1, TermekVariansId = 1 });
            await context.SaveChangesAsync();

            var controller = new RendelesekController(context);

            // Act
            var result = await controller.GetStats();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task PostRendeles_SikeresVasarolas_LevonjaAKeszletetEsUritiAKosarat()
        {
            // Arrange
            using var context = GetDbContext();

            var felhasznalo = new Felhasznalo { Email = "vevo@gmail.com", UserName = "Vevo1", PasswordHash = "pw" };
            context.Felhasznalok.Add(felhasznalo);

            var termek = new Termek { Nev = "Fesztivál Jegy", Ar = 15000, Tipus = "Jegy" };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var varians = new TermekVarians { TermekId = termek.Id, Keszlet = 100 };
            context.TermekVariansok.Add(varians);

            context.Kosarak.Add(new Kosar { FelhasznaloId = felhasznalo.Id, TermekVariansId = varians.Id, Mennyiseg = 2 });
            await context.SaveChangesAsync();

            var controller = new RendelesekController(context);

            var dto = new RendelesPostDto
            {
                FelhasznaloId = felhasznalo.Id,
                Vegosszeg = 30000,
                Tetelek = new List<RendelesTetelPostDto>
                {
                    new RendelesTetelPostDto { TermekVariansId = varians.Id, Mennyiseg = 2, Egysegar = 15000 }
                }
            };

            // Act
            var result = await controller.PostRendeles(dto);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var frissitve = await context.TermekVariansok.FindAsync(varians.Id);
            Assert.Equal(98, frissitve.Keszlet);

            var kosarDb = await context.Kosarak.CountAsync(k => k.FelhasznaloId == felhasznalo.Id);
            Assert.Equal(0, kosarDb);

            Assert.Equal(1, await context.Rendelesek.CountAsync());
        }

        [Fact]
        public async Task PostRendeles_NincsElegKeszlet_HibavalTerVissza()
        {
            // Arrange
            using var context = GetDbContext();
            var termek = new Termek { Nev = "Elfogyó Termék", Ar = 1000, Tipus = "Teszt" };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var varians = new TermekVarians { TermekId = termek.Id, Keszlet = 1 };
            context.TermekVariansok.Add(varians);
            await context.SaveChangesAsync();

            var controller = new RendelesekController(context);
            var dto = new RendelesPostDto
            {
                FelhasznaloId = 1,
                Vegosszeg = 5000,
                Tetelek = new List<RendelesTetelPostDto>
                {
                    new RendelesTetelPostDto { TermekVariansId = varians.Id, Mennyiseg = 5, Egysegar = 1000 }
                }
            };

            // Act
            var result = await controller.PostRendeles(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Nincs elég készlet", badRequest.Value.ToString());
        }

        #endregion

        #region KEMPINGEK CONTROLLER TESZTEK

        [Fact]
        public async Task GetKempingek_MindenKempingetVisszaad()
        {
            // Arrange
            using var context = GetDbContext();
            context.Kempingek.AddRange(new List<Kemping> {
                new Kemping { Nev = "Északi Kemping", Leiras = "Hideg de szép", KepUrl = "eszak.jpg", Ar = 5000 },
                new Kemping { Nev = "Déli Kemping", Leiras = "Meleg és napos", KepUrl = "del.jpg", Ar = 4500 }
            });
            await context.SaveChangesAsync();
            var controller = new KempingekController(context);

            // Act
            var result = await controller.GetKempingek();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Kemping>>>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Kemping>>(actionResult.Value);
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task GetKemping_LetezoIdre_MegfeleloHelyetAd()
        {
            // Arrange
            using var context = GetDbContext();
            var kemping = new Kemping
            {
                Nev = "Prémium Kemping",
                Leiras = "Luxus sátorhely",
                KepUrl = "premium.jpg",
                Ar = 12000
            };
            context.Kempingek.Add(kemping);
            await context.SaveChangesAsync();
            var controller = new KempingekController(context);

            // Act
            var result = await controller.GetKemping(kemping.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var visszakapott = Assert.IsType<Kemping>(okResult.Value);
            Assert.Equal("Prémium Kemping", visszakapott.Nev);
            Assert.Equal(12000, visszakapott.Ar);
        }

        [Fact]
        public async Task GetKemping_NemLetezoIdre_NotFoundotAdUzenettel()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new KempingekController(context);

            // Act
            var result = await controller.GetKemping(999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("999", notFoundResult.Value.ToString());
        }

        #endregion

        #region HOSTELEK CONTROLLER TESZTEK

        [Fact]
        public async Task GetHostelek_MindenHosteltVisszaad()
        {
            // Arrange
            using var context = GetDbContext();
            context.Hostelek.AddRange(new List<Hostel> {
                new Hostel { Nev = "Városi Hostel", Leiras = "A központban", KepUrl = "hostel1.jpg", Ar = 8000, Link = "https://hostel1.hu" },
                new Hostel { Nev = "Parti Szálló", Leiras = "Vízparti kilátás", KepUrl = "hostel2.jpg", Ar = 9500 }
            });
            await context.SaveChangesAsync();
            var controller = new HostelekController(context);

            // Act
            var result = await controller.GetHostelek();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Hostel>>>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Hostel>>(actionResult.Value);
            Assert.Equal(2, list.Count());
        }

        #endregion

        #region FELHASZNÁLÓK CONTROLLER TESZTEK

        [Fact]
        public async Task GetFelhasznalok_VisszaadjaAzOsszesRegisztraltat()
        {
            // Arrange
            using var context = GetDbContext();
            context.Felhasznalok.AddRange(new List<Felhasznalo> {
                new Felhasznalo { UserName = "user1", Email = "u1@test.hu", PasswordHash = "hash1", Role = "User" },
                new Felhasznalo { UserName = "admin1", Email = "admin@test.hu", PasswordHash = "hash2", Role = "Admin" }
            });
            await context.SaveChangesAsync();
            var controller = new FelhasznalokController(context);

            // Act
            var result = await controller.GetFelhasznalok();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<System.Collections.IEnumerable>(okResult.Value);

            int count = 0;
            foreach (var item in list) count++;
            Assert.Equal(2, count);
        }

        #endregion

        #region TERMÉK ÉS VARIÁNS TESZTEK

        [Fact]
        public async Task GetTermekek_VariansokkalEgyutt_HelyesKeszletetAd()
        {
            // Arrange
            using var context = GetDbContext();

            var meret = new Meret { Megnevezes = "L" };
            context.Meretek.Add(meret);
            await context.SaveChangesAsync();

            var termek = new Termek
            {
                Nev = "Fesztivál Póló",
                Ar = 5000,
                Tipus = "Merch",
                KepUrl = "polo.jpg"
            };
            context.Termekek.Add(termek);
            await context.SaveChangesAsync();

            var varians = new TermekVarians
            {
                TermekId = termek.Id,
                MeretId = meret.Id,
                Keszlet = 15
            };
            context.TermekVariansok.Add(varians);
            await context.SaveChangesAsync();

            var controller = new TermekekController(context);

            // Act
            var result = await controller.GetTermekek();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Termek>>(okResult.Value);
            var tesztTermek = list.FirstOrDefault(t => t.Nev == "Fesztivál Póló");

            Assert.NotNull(tesztTermek);
            Assert.NotEmpty(tesztTermek.Variansok);
            Assert.Equal(15, tesztTermek.Variansok.First().Keszlet);
        }

        #endregion
    }
}