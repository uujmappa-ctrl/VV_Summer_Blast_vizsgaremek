using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VVSummerBlastFRONTEND.Dtos;
using VVSummerBlastFRONTEND.Services;

namespace VVSummerBlastFRONTEND.Pages.Idorend
{
    public class IndexModel : PageModel
    {
        private readonly EsemenyApi _esemenyApi;

        [BindProperty(SupportsGet = true)]
        public string? Mufaj { get; set; }

        public Dictionary<string, List<EsemenyDto>> NapiProgram { get; set; } = new();

        public List<string> Mufajok { get; set; } = new()
        {
            "pop", "rap", "rock", "indie", "alternativ", "elektronikus", "hiphop"
        };

        public IndexModel(EsemenyApi esemenyApi) => _esemenyApi = esemenyApi;

        public async Task OnGetAsync()
        {
            var esemenyek = await _esemenyApi.GetEsemenyekAsync(Mufaj);

            NapiProgram = esemenyek
                .OrderBy(e => e.Kezdes)
                .GroupBy(e => e.Kezdes.ToString("yyyy-MM-dd"))
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
