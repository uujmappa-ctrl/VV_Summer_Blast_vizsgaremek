using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    // Általános termék modell (póló, pulóver, bérlet, stb.)
    public class Termek
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nev { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ar { get; set; }

        [Required]
        public string Tipus { get; set; } // Megkülönböztetésre: Pl. "ruházat", "jegy"

        public string KepUrl { get; set; }

        [Required]
        public int Keszlet { get; set; } // készletmennyiség
    }
}