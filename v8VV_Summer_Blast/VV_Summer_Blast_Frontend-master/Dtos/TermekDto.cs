namespace VVSummerBlastFRONTEND.Dtos
{
    public class TermekDto
    {
        public int Id { get; set; }
        public string Nev { get; set; } = string.Empty;
        public string Leiras { get; set; } = string.Empty;
        public decimal Ar { get; set; }
        public int Keszlet { get; set; } // Összesített készlet (vagy az alapvariánsé)
        public string KepUrl { get; set; } = string.Empty;
        public string Tipus { get; set; } = string.Empty; // Pl: "Ruházat", "Kiegészítő"

        // A termék választható méretei/változatai
        public List<TermekVariansDto> Variansok { get; set; } = new();
    }
}