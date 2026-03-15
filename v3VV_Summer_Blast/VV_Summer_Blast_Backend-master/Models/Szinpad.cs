using System.ComponentModel.DataAnnotations;

namespace VVSummerBlastBackendAPI.Models
{
    public class Szinpad
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nev { get; set; }

        [MaxLength(150)]
        public string Helyszin { get; set; } // A színpad pontos helye a fesztivál térképén

        // Az adott színpadon zajló összes koncert/esemény listája
        public virtual ICollection<Esemeny> Esemenyek { get; set; } = new List<Esemeny>();
    }
}