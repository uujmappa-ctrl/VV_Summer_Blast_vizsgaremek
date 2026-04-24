namespace VVSummerBlastBackendAPI.Models
{
    // A fesztivál területén belüli kemping opciók
    public class Kemping
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public string KepUrl { get; set; }
        public int Ar { get; set; }
    }
}