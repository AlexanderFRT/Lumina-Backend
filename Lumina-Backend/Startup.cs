using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lumina_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Lumina_Backend.Options;
using Lumina_Backend.Middleware;

namespace Lumina_Backend;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Autenticación con JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true; // Require HTTPS metadata

            // Recuperar la clave secreta JWT de la configuración
            var secretKey = _configuration["JwtSettings:SecretKey"];

            // Comprobar si la clave secreta no es nula ni está vacía
            if (string.IsNullOrEmpty(secretKey))
            {
                // Lanzar una excepción si la clave secreta no está configurada
                throw new InvalidOperationException("La clave secreta JWT no está configurada.");
            }

            // Convertir la clave secreta a bytes
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                IssuerSigningKey = signingKey,
            };
        });

        // Middleware de límite de tasa
        services.AddLogging(builder =>
        {
            builder.AddConsole(); // Add console logger provider
            builder.AddDebug();   // Add debug logger provider
        });
        services.Configure<RateLimitOptions>(_configuration.GetSection("RateLimit"));
        services.AddSingleton<IRateLimitCounter, MemoryCacheRateLimitCounter>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts(); // Enable HSTS (HTTP Strict Transport Security)
        }

        // Redirect HTTP requests to HTTPS
        app.UseHttpsRedirection();

        // Middleware de límite de tasa
        app.UseMiddleware<RateLimitMiddleware>();

        app.UseRouting();

        // Middleware de autenticación
        app.UseAuthentication();
        app.UseAuthorization();

        // Otras configuraciones de middleware

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}