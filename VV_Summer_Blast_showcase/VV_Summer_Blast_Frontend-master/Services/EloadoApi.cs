using VVSummerBlastFRONTEND.Dtos;
using System.Net.Http.Json;

namespace VVSummerBlastFRONTEND.Services
{


    public class EloadoApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EloadoApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            // A backend URL lekérése az appsettings.json fájlból, fallback értékkel
            _baseUrl = configuration.GetValue<string>("BackendSettings:BaseUrl") ?? "https://localhost:7025/";
        }

 
        // Lekéri az összes előadót, opcionálisan műfaj alapján szűrve.
     
        public async Task<List<EloadoDto>> GetEloadokAsync(string? mufaj = null)
        {
            // Biztonságos URL építés: levágjuk a felesleges / jeleket a végéről
            var url = $"{_baseUrl.TrimEnd('/')}/api/Eloadok";

            // Query string paraméter hozzáadása, ha van megadott műfaj
            if (!string.IsNullOrWhiteSpace(mufaj))
            {
                url += $"?mufaj={Uri.EscapeDataString(mufaj)}";
            }

            try
            {
                // Deszerializáció közvetlenül DTO listává
                var result = await _httpClient.GetFromJsonAsync<List<EloadoDto>>(url);
                return result ?? new List<EloadoDto>();
            }
            catch (Exception ex)
            {
                // (Hibanaplózás konzolra)
                Console.WriteLine($"Hiba az API hívásakor (GetEloadok): {ex.Message}");
                return new List<EloadoDto>();
            }
        }


        // Egy konkrét előadó adatlapjának lekérése egyedi azonosító alapján:
        public async Task<EloadoDto?> GetEloadoByIdAsync(int id)
        {
            var url = $"{_baseUrl.TrimEnd('/')}/api/Eloadok/{id}";

            try
            {
                return await _httpClient.GetFromJsonAsync<EloadoDto>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az API hívásakor (GetEloadoById - ID: {id}): {ex.Message}");
                return null;
            }
        }
    }
}