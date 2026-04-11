using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VVSummerBlastBackendAPI.Models
{
    // Ez a modell kezeli a készletet méretenként (pl. M-es póló, L-es póló)
    public class TermekVarians
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TermekId { get; set; }

        [ForeignKey("TermekId")]
        [JsonIgnore] // Elkerüljük a körkörös hivatkozást az API válaszban
        public virtual Termek? Termek { get; set; }

        [Required]
        public int MeretId { get; set; }

        [ForeignKey("MeretId")]
        public virtual Meret? Meret { get; set; }

        [Required]
        public int Keszlet { get; set; } // Az admin felületen ezt módosítjuk

        public virtual ICollection<Kosar>? Kosarak { get; set; } = new List<Kosar>();
        public virtual ICollection<RendelesTetel>? RendelesTetelek { get; set; } = new List<RendelesTetel>();
    }
}