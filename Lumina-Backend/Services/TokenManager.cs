using Lumina_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lumina_Backend.Services;

public class TokenManager
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenManager> _logger;

    public TokenManager(IConfiguration configuration, ILogger<TokenManager> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes), // Establecer el tiempo de expiración inicial
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se produjo un error al generar el token JWT.");
            throw;
        }
    }

    // Método para actualizar el tiempo de expiración del token
    public string RefreshTokenExpiration(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenExpirationMinutes = Convert.ToInt32(jwtSettings["TokenExpirationMinutes"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var originalToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var now = DateTime.UtcNow;

            if (originalToken == null)
            {
                throw new InvalidOperationException("Token inválido. No se puede actualizar el tiempo de expiración.");
            }

            // Crear identidad de reclamación/propiedad a partir del user del token original
            var claimsIdentity = new ClaimsIdentity(originalToken.Claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = now.AddMinutes(tokenExpirationMinutes), // Actualizar tiempo de expiración
                SigningCredentials = originalToken.SigningCredentials
            };

            var refreshedToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(refreshedToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se produjo un error al actualizar el tiempo de expiración del token JWT.");
            throw;
        }
    }
}