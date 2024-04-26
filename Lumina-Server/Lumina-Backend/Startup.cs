using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lumina_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Lumina_Backend.Options;
using Lumina_Backend.Middleware;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Lumina_Backend;

public class Startup(IConfiguration configuration, ILogger<Startup> logger)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<Startup> _logger = logger;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("EnableNetlify", builder =>
            {
                builder.WithOrigins("https://luminabank.netlify.app", "http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });

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
            throw;
        }

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        services.Configure<RateLimitOptions>(_configuration.GetSection("RateLimit"));
        services.AddSingleton<IRateLimitCounter, MemoryCacheRateLimitCounter>();
        services.AddSingleton<TokenManager>();

        services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
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
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseMiddleware<RateLimitMiddleware>();
        app.UseCors("EnableNetlify");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
