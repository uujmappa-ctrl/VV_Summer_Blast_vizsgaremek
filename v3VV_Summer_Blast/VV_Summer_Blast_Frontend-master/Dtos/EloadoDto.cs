namespace VVSummerBlastFRONTEND.Dtos
{
    public class EloadoDto
    {
        public int Id { get; set; }
        public string Nev { get; set; } = string.Empty;
        public string Mufaj { get; set; } = string.Empty;
        public string Leiras { get; set; } = string.Empty;
        public string KepUrl { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty; // Részletesebb életrajz a profiloldalhoz
        public string SpotifyUrl { get; set; } = string.Empty; // Közvetlen link a lejátszóhoz
    }
}