using Lumina_Backend.Data;
using Lumina_Backend.Middleware;
using Lumina_Backend.Options;
using Lumina_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Lumina_Backend;

public class Program
{
    public static void Main(string[] args)
    {
        // Método para generar nuevas JWT Secret Keys usando un generador criptográfico de números aleatorios
        /* byte[] keyBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(keyBytes);
        }

        string key = Convert.ToBase64String(keyBytes);
        Console.WriteLine(key); */

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
     
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApiDbContext>(opt =>
                opt.UseNpgsql(connectionString));

        //  Cuando ya se vaya a hacer el despliegue local en Docker de la versión final del DB se elimina la env variable connectionString y se modifica el servicio nuevamente con el codigo de abajo, para usar el localhost string del appsettings.json
        //      opt.UseNpgsql(builder.Configuration.GetConnectionString("LuminaConnection")));

        //Builder para el RateLimit Middleware
        builder.Services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection("RateLimit"));
        builder.Services.AddSingleton<IRateLimitCounter, MemoryCacheRateLimitCounter>();
        builder.Services.AddSingleton<TokenManager>();

        // Builder para el JWT
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication("Bearer").AddJwtBearer();

        //builder.Services.AddTransient<IUserRepository, UserRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<RateLimitMiddleware>();
        app.UseRouting();
        app.UseAuthorization(); 
        app.MapControllers();
        app.Run();
    }
}