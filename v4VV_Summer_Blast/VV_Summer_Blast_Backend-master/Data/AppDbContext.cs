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

        // Termékkezelés
        public DbSet<Termek> Termekek { get; set; }

        // Szálláshelyek kezelése
        public DbSet<Hostel> Hostelek { get; set; }
        public DbSet<Kemping> Kempingek { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pénzügyi pontosság beállítása
            modelBuilder.Entity<Termek>()
                .Property(t => t.Ar)
                .HasPrecision(18, 2);
        }
    }
}