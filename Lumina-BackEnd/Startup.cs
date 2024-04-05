using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lumina_BackEnd
{

    public class Startup
    {
        //private IConfiguration Configuration { get; }

        //private string key = "TjMqKHHEHUA2vNu4Cd3SdBEOd7YbP7vy";
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var jwtSettings = Configuration.GetSection("JwtSettings");

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
        //    }).AddJwtBearer(options =>{
        //        options.RequireHttpsMetadata = false;
        //        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSettings:SecretKey").Value!));
        //        var signingCredentialas = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,                
        //            IssuerSigningKey = signingKey,
        //        };
        //    });

            

        //}
    }
}
