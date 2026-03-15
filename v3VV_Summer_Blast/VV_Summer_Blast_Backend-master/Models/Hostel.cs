namespace VVSummerBlastBackendAPI.Models
{
    // A fesztivál környéki fix szálláshelyek (pl. kollégiumok) modellje
    public class Hostel
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public string KepUrl { get; set; }
        public int Ar { get; set; }

        // Külső weboldalra mutató link, ha ott kell foglalni
        public string? Link { get; set; }
    }
}