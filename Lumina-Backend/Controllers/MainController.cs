using static Lumina_Backend.Controllers.AccountController;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController : ControllerBase
{
    protected UserId GetAuthenticatedUserId()
    {
        var token = HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("El token JWT no fue encontrado en la solicitud.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken ?? throw new InvalidOperationException("El token JWT no es válido.");
        var userId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("El ID del usuario no fue encontrado en el Token JWT.");
        }

        if (!int.TryParse(userId, out int parsedUserId))
        {
            throw new InvalidOperationException("El ID del usuario no es válido.");
        }

        return new UserId { Id = parsedUserId };
    }
}
