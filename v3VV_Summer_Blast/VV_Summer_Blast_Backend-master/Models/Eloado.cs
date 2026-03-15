using System.ComponentModel.DataAnnotations;

namespace VVSummerBlastBackendAPI.Models
{
    public class Eloado
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nev { get; set; }

        [MaxLength(1000)]
        public string Leiras { get; set; }

        public string KepUrl { get; set; } // A feltöltött kép elérési útja

        [MaxLength(50)]
        public string Mufaj { get; set; }

        public string? Bio { get; set; }
        public string? SpotifyUrl { get; set; }

        // Navigációs tulajdonság a fellépésekhez
        public virtual ICollection<Esemeny> Esemenyek { get; set; } = new List<Esemeny>();
    }
}