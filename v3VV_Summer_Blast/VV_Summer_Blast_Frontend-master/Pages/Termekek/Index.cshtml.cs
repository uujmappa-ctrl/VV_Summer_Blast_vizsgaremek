using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VVSummerBlastFRONTEND.Dtos;
using VVSummerBlastFRONTEND.Services;

namespace VVSummerBlastFRONTEND.Pages.Termekek
{
    public class IndexModel : PageModel
    {
        private readonly TermekApi _termekApi;

        [BindProperty(SupportsGet = true)]
        public string Kategoria { get; set; }

        public List<TermekDto> Termekek { get; set; } = new();

        public IndexModel(TermekApi termekApi) => _termekApi = termekApi;

        public async Task OnGetAsync()
        {
            var minden = await _termekApi.GetMindenTermekAsync();

            var alapSzurt = minden.Where(t => t.Tipus != "Jegy");

            if (!string.IsNullOrEmpty(Kategoria))
            {
                Termekek = alapSzurt.Where(t => t.Tipus.ToLower() == Kategoria.ToLower()).ToList();
            }
            else
            {
                Termekek = alapSzurt.ToList();
            }
        }
    }
}
