using VVSummerBlastFRONTEND.Services;

namespace VVSummerBlastFRONTEND
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient<EloadoApi>();
            builder.Services.AddHttpClient<VVSummerBlastFRONTEND.Services.TermekApi>();
            builder.Services.AddHttpClient<EsemenyApi>();
            builder.Services.AddHttpClient<SzallasApi>();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();


            app.Run();
        }
    }
}
