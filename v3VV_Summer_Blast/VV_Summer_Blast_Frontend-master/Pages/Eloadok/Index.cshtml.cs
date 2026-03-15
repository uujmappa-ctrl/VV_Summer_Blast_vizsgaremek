using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VVSummerBlastFRONTEND.Dtos;
using VVSummerBlastFRONTEND.Services;

namespace VVSummerBlastFRONTEND.Pages.Eloadok
{
    public class IndexModel : PageModel
    {
        private readonly EloadoApi _eloadoApi;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public string? Mufaj { get; set; }

        public List<EloadoDto> Eloadok { get; set; } = new();

        public List<string> Mufajok { get; set; } = new()
        {
            "Rock", "Pop", "Elektronikus", "Rap", "Indie", "Alternativ", "HipHop" // Ide írd be a valós műfajaidat!
        };

        public IndexModel(EloadoApi eloadoApi, IConfiguration configuration)
        {
            _eloadoApi = eloadoApi;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            Eloadok = await _eloadoApi.GetEloadokAsync(Mufaj);

            ViewData["BackendBaseUrl"] = _configuration.GetValue<string>("BackendSettings:BaseUrl")
                                          ?? "https://localhost:7025";
        }
    }
}
