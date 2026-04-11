using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Alapadatok: Felhasználók, előadók és helyszínek
        public DbSet<Felhasznalo> Felhasznalok { get; set; }
        public DbSet<Eloado> Eloadok { get; set; }
        public DbSet<Szinpad> Szinpadok { get; set; }
        public DbSet<Esemeny> Esemenyek { get; set; }

        // Termékkezelés: Alaptermékek és azok variánsai (pl. különböző méretek)
        public DbSet<Termek> Termekek { get; set; }
        public DbSet<Meret> Meretek { get; set; }
        public DbSet<TermekVarians> TermekVariansok { get; set; }

        // Értékesítési folyamat: Kosár és a véglegesített rendelések adatai
        public DbSet<Kosar> Kosarak { get; set; }
        public DbSet<Rendeles> Rendelesek { get; set; }
        public DbSet<RendelesTetel> RendelesTetelek { get; set; }

        // Szálláshelyek kezelése
        public DbSet<Hostel> Hostelek { get; set; }
        public DbSet<Kemping> Kempingek { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pénzügyi pontosság beállítása: 
            // Biztosítjuk, hogy az árak és összegek 18 számjegyet tároljanak, amiből 2 a tizedesjegy.

            modelBuilder.Entity<Termek>()
                .Property(t => t.Ar)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Rendeles>()
                .Property(r => r.Vegosszeg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RendelesTetel>()
                .Property(rt => rt.Egysegar)
                .HasPrecision(18, 2);
        }
    }
}