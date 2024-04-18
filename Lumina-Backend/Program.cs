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

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("EnableNetlify", builder =>
            {
                builder.WithOrigins("https://luminabank.netlify.app")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });

        builder.Services.AddEntityFrameworkNpgsql()
        .AddDbContext<ApiDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("LuminaConnection"))); 

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection("RateLimit"));
        builder.Services.AddSingleton<IRateLimitCounter, MemoryCacheRateLimitCounter>();
        builder.Services.AddCors();
        builder.Services.AddAuthentication("Bearer").AddJwtBearer();
        builder.Services.AddSingleton<TokenManager>();
        builder.Services.AddAuthorization();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseMiddleware<RateLimitMiddleware>();
        app.UseCors("EnableNetlify");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}