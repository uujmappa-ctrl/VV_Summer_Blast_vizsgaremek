using Microsoft.AspNetCore.Mvc.RazorPages;
using VVSummerBlastFRONTEND.Dtos;
using VVSummerBlastFRONTEND.Services;

namespace VVSummerBlastFRONTEND.Pages.Kemping
{
    public class IndexModel : PageModel
    {
        private readonly SzallasApi _szallasApi;
        public List<KempingDto> Kempingek { get; set; } = new();

        public IndexModel(SzallasApi szallasApi) => _szallasApi = szallasApi;

        public async Task OnGetAsync()
        {
            try
            {
                Kempingek = await _szallasApi.GetKempingekAsync();
            }
            catch
            {
                Kempingek = new List<KempingDto>();
            }
        }
    }
}
