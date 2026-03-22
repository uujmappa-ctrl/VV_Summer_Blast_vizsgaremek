using VVSummerBlastFRONTEND.Dtos;
using System.Net.Http.Json;

namespace VVSummerBlastFRONTEND.Services
{
    /// <summary>
    /// A fesztivál shop termékeit és a belépőjegyeket kezelő API szerviz osztály.
    /// </summary>
    public class TermekApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TermekApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // API alapcím beolvasása a konfigurációból
            _baseUrl = configuration.GetValue<string>("BackendSettings:BaseUrl") ?? "https://localhost:7025/";
        }

        /// <summary>
        /// Lekéri a backendről az összes elérhető terméket (merch, jegy, stb.).
        /// </summary>
        public async Task<List<TermekDto>> GetMindenTermekAsync()
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Termekek";
                return await _httpClient.GetFromJsonAsync<List<TermekDto>>(url) ?? new List<TermekDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a termékek lekérésekor: {ex.Message}");
                return new List<TermekDto>();
            }
        }

        /// <summary>
        /// Lekéri csak a jegy típusú termékeket, de továbbra is TermekDto formátumban tartja őket.
        /// </summary>
        public async Task<List<TermekDto>> GetJegyekAsync()
        {
            // Lekérjük az összes terméket
            var osszesTermek = await GetMindenTermekAsync();

            // Csak azokat tartjuk meg, amiknek a típusa "Jegy" (ID-tól függetlenül)
            // Így a lista struktúrája ugyanaz marad, mint a shopnál, csak a tartalom szűrt
            return osszesTermek
                .Where(t => t.Tipus != null && t.Tipus.Equals("Jegy", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}