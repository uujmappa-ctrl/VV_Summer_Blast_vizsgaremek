using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    // Ez a tábla bontja meg a rendelést konkrét termékekre és mennyiségekre
    public class RendelesTetel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RendelesId { get; set; }
        [ForeignKey("RendelesId")]
        public virtual Rendeles Rendeles { get; set; } = null!;

        [Required]
        public int TermekVariansId { get; set; }
        [ForeignKey("TermekVariansId")]
        public virtual TermekVarians TermekVarians { get; set; } = null!;

        [Required]
        public int Mennyiseg { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Egysegar { get; set; } // Az eladáskori árat rögzítjük, ha később változna a termék ára
    }
}