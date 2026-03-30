using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Models
{
    // A bejelentkezett felhasználók kosara, adatbázisban tárolva
    public class Kosar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FelhasznaloId { get; set; }
        [ForeignKey("FelhasznaloId")]
        public virtual Felhasznalo Felhasznalo { get; set; }

        [Required]
        public int TermekId { get; set; } 
        [ForeignKey("TermekId")]
        public virtual Termek Termek { get; set; }

        [Range(1, 50)]
        public int Mennyiseg { get; set; }
    }
}