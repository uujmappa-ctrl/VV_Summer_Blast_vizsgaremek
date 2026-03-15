namespace VVSummerBlastFRONTEND.Dtos
{
    public class HostelDto
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public string KepUrl { get; set; }
        public int Ar { get; set; } // Éjszakánkénti ár
        public string? Link { get; set; } // Opcionális külső foglalási link
    }
}