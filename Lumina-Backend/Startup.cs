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
    private readonly ILogger<Startup> _logger;

    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Autenticación con JWT
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var tokenExpirationMinutes = Convert.ToInt32(jwtSettings["TokenExpirationMinutes"]);

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("La clave secreta de JWT no está configurada.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se produjo un error al configurar la autenticación JWT.");
            throw; // Vuelva a lanzar la excepción para que la aplicación la maneje
        }

        // Middleware de límite de tasa
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
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
            app.UseHsts(); // Habilita HSTS (HTTP Strict Transport Security)
        }

        // Redirecciona las requests HTTP a HTTPS
        app.UseHttpsRedirection();

        // Middleware de límite de tasa
        app.UseMiddleware<RateLimitMiddleware>();

        app.UseRouting();

        // Middleware de autenticación
        app.UseAuthentication();
        app.UseAuthorization();

        // Otras configuraciones generales de middleware

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}