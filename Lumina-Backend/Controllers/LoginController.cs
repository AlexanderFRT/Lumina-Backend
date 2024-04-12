using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Data;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ApiDbContext _context;

    public LoginController(ApiDbContext context)
    {
        _context = context;
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

        // Inicio de sesión exitoso
        // Se va a generar un token JWT aquí para autenticación también
        // Por ahora se devuelve un mensaje de éxito
        return Ok("Inicio de sesión exitoso.");
    }

    // Representa una solicitud de inicio de sesión con credenciales de usuario
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}