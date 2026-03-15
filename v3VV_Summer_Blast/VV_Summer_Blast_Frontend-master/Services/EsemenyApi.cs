using VVSummerBlastFRONTEND.Dtos;
using System.Net.Http.Json;

namespace VVSummerBlastFRONTEND.Services
{

    // A fesztivál eseményei (koncertek, programok)

    public class EsemenyApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EsemenyApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // A backend elérhetősége az appsettings.json-ból származik
            _baseUrl = configuration.GetValue<string>("BackendSettings:BaseUrl") ?? "https://localhost:7025/";
        }

        // Lekéri az események listáját, opcionális műfajokra szűrve
  
        public async Task<List<EsemenyDto>> GetEsemenyekAsync(string? mufaj = null)
        {
            try
            {
                var url = $"{_baseUrl.TrimEnd('/')}/api/Esemenyek";

                if (!string.IsNullOrWhiteSpace(mufaj))
                {
                    url += $"?mufaj={Uri.EscapeDataString(mufaj)}";
                }

                // Stringként kérjük le a választ a rugalmasabb JSON feldolgozás érdekében
                var response = await _httpClient.GetStringAsync(url);

                // Beállítjuk, hogy a kis- és nagybetű eltérés ne okozzon gondot a JSON mappingnél
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return System.Text.Json.JsonSerializer.Deserialize<List<EsemenyDto>>(response, options)
                       ?? new List<EsemenyDto>();
            }
            catch (Exception ex)
            {
                // Hiba esetén üres listát adunk vissza
                Console.WriteLine($"Hiba az események lekérésekor: {ex.Message}");
                return new List<EsemenyDto>();
            }
        }
    }
}