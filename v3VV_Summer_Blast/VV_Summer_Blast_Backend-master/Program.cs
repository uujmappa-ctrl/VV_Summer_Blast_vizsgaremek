using Microsoft.EntityFrameworkCore;
using VVSummerBlastBackendAPI.Data;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Adatbázis kapcsolat beállítása (MySQL/MariaDB) a konfigurációs fájlból
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Megakadályozzuk, hogy az egymásba ágyazott objektumok (pl. Rendelés -> Tétel -> Rendelés) végtelen ciklust okozzanak
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        // A null értékû mezõket bele se teszi a JSON válaszba, így tisztább marad a kimenet
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS beállítása: Engedélyezzük, hogy a frontend (vagy bárki más) elérje az API-t
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger felület bekapcsolása fejlesztõi módban a teszteléshez
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // Statikus fájlok (pl. képek) kiszolgálása
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Adatbázis automatikus elõkészítése indításkor
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Lefuttatja a hiányzó migrációkat (létrehozza a táblákat, ha nincsenek)
    context.Database.Migrate();

    // Feltölti az adatbázist alapértelmezett tesztadatokkal
    DbSeeder.Seed(context);
}

app.Run();