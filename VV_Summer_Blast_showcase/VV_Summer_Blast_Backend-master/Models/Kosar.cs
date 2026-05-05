using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VVSummerBlastBackendAPI.Models;

// A bejelentkezett felhasználók kosara, adatbázisban tárolva a perzisztencia miatt
public class Kosar
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FelhasznaloId { get; set; }
    [ForeignKey("FelhasznaloId")]
    public virtual Felhasznalo Felhasznalo { get; set; }

    [Required]
    public int TermekVariansId { get; set; } // Pontos méret/típus beazonosítása
    [ForeignKey("TermekVariansId")]
    public virtual TermekVarians TermekVarians { get; set; }

    [Range(1, 50)]
    public int Mennyiseg { get; set; }
}