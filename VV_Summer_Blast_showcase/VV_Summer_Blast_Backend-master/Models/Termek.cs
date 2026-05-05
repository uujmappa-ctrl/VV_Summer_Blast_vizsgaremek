using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    // Általános termék modell (lehet póló, pulóver, de akár VIP bérlet is)
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
        public string Tipus { get; set; } // Megkülönböztetésre: Pl. "Ruházat", "Belépő"

        public string KepUrl { get; set; }

        // A termékhez tartozó különböző méretváltozatok elérése
        public virtual ICollection<TermekVarians> Variansok { get; set; } = new List<TermekVarians>();
    }
}