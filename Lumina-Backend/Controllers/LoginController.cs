using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lumina_Backend.Services;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly TokenManager _tokenManager;

    public LoginController(ApiDbContext context, TokenManager tokenManager)
    {
        _context = context;
        _tokenManager = tokenManager;
    }

    // POST: api/Login https://localhost:7024/api/Login
    // Endpoint para iniciar sesión
    [HttpPost]
    public IActionResult PostLogin([FromBody] LoginRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Se requieren nombre de usuario y contraseña.");
        }

        var user = _context.Users.FirstOrDefault(u => u.UserName == request.UserName);
        if (user == null)
        {
            return NotFound("Nombre de usuario no encontrado.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized("Contraseña incorrecta.");
        }

        // Genera el token JWT
        var token = _tokenManager.GenerateToken(user);

        // Guarda el token generado para el usuario en el DB
        user.SessionToken = token;
        _context.SaveChanges();

        // Regresa el token y un mensaje de "Inicio de sesión exitoso"
        return Ok(new { Token = token, Message = "Inicio de sesión exitoso." });
    }

    // Representa una solicitud de inicio de sesión con credenciales de usuario
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}