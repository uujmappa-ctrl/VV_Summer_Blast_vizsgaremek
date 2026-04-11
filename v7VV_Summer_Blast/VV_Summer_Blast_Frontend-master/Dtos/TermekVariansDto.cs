namespace VVSummerBlastFRONTEND.Dtos
{
    public class TermekVariansDto
    {
        public int Id { get; set; } // Ez a konkrét variáns (méret) egyedi azonosítója
        public int TermekId { get; set; }
        public int MeretId { get; set; }
        public int Keszlet { get; set; } // Ez mutatja, mennyi van pont ebből a méretből

        // A méret részletes adatai (ha a frontendnek szüksége van a megnevezésre is)
        public MeretDto? Meret { get; set; }
    }
}