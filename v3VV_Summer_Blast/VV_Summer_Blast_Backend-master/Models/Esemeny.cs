using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    // Ez a tábla kapcsolja össze az előadókat a színpadokkal és időpontokkal
    public class Esemeny
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EloadoId { get; set; }
        [ForeignKey("EloadoId")]
        public virtual Eloado Eloado { get; set; }

        [Required]
        public int SzinpadId { get; set; }
        [ForeignKey("SzinpadId")]
        public virtual Szinpad Szinpad { get; set; }

        [Required]
        public DateTime Kezdes { get; set; }

        [Required]
        public DateTime Vege { get; set; }

        [MaxLength(500)]
        public string Leiras { get; set; }
    }
}