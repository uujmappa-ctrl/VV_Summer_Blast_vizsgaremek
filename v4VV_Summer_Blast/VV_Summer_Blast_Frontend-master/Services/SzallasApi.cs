using VVSummerBlastFRONTEND.Dtos;
using System.Net.Http.Json;

namespace VVSummerBlastFRONTEND.Services
{
    /// <summary>
    /// A fesztivál szálláshelyeinek (Hostelek és Kempingek) adatait kiszolgáló API szerviz.
    /// </summary>
    public class SzallasApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SzallasApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // A központi API cím beolvasása konfigurációból
            _baseUrl = configuration.GetValue<string>("BackendSettings:BaseUrl") ?? "https://localhost:7025/";
        }

        /// <summary>
        /// Lekéri az összes elérhető hostelt az adatbázisból.
        /// </summary>
        public async Task<List<HostelDto>> GetHostelekAsync()
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Hostelek";
                // Közvetlen deszerializáció a megadott DTO típusra
                return await _httpClient.GetFromJsonAsync<List<HostelDto>>(url) ?? new List<HostelDto>();
            }
            catch (Exception ex)
            {
                // Hibanaplózás a könnyebb debugolás érdekében
                Console.WriteLine($"Hostel lekérdezési hiba: {ex.Message}");
                return new List<HostelDto>();
            }
        }

        /// <summary>
        /// Lekéri az összes elérhető kemping helyszínt és típust.
        /// </summary>
        public async Task<List<KempingDto>> GetKempingekAsync()
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Kempingek";
                return await _httpClient.GetFromJsonAsync<List<KempingDto>>(url) ?? new List<KempingDto>();
            }
            catch (Exception ex)
            {
                // Hiba esetén üres listával térünk vissza, megelőzve a null-reference hibákat a UI-on
                Console.WriteLine($"Kemping lekérdezési hiba: {ex.Message}");
                return new List<KempingDto>();
            }
        }
    }
}