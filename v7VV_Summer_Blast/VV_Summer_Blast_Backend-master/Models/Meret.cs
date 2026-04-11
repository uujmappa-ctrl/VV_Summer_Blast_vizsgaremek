using System.ComponentModel.DataAnnotations;

namespace VVSummerBlastBackendAPI.Models
{
    // Segédtábla a választható méreteknek (pl. S, M, L, XL vagy Napijegy, Bérlet)
    public class Meret
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Megnevezes { get; set; }
    }
}