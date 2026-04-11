using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    public class Rendeles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FelhasznaloId { get; set; }
        [ForeignKey("FelhasznaloId")]
        public virtual Felhasznalo Felhasznalo { get; set; }

        public DateTime RendelesIdeje { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Pénzügyi adatokhoz precízebb a decimal
        public decimal Vegosszeg { get; set; }

        public string Statusz { get; set; } // Pl: Feldolgozás alatt, Teljesítve

        // Egy rendeléshez több tétel (termék) is tartozhat
        public virtual ICollection<RendelesTetel> RendelesTetelek { get; set; } = new List<RendelesTetel>();
    }
}