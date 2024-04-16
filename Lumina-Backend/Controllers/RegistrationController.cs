using Lumina_Backend.Data;
using Lumina_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Lumina_Backend.Models.BaseEntity;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly ApiDbContext _context;

    public RegistrationController(ApiDbContext context)
    {
        _context = context;
    }

    // POST: api/Registration https://localhost:7024/api/Registration
    // Endpoint para registrarse
    [HttpPost]
    public async Task<IActionResult> PostUser(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Chequea si el nombre de usuario está tomado
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName))
        {
            ModelState.AddModelError("UserName", "El nombre de usuario ya está en uso.");
            return BadRequest(ModelState);
        }

        // Chequea si el email ya está registrado
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
            return BadRequest(ModelState);
        }

        // Encriptar la contraseña antes de almacenarla en la base de datos por motivos de seguridad
        string hashedPassword = HashPassword(request.Password);

        // Creación del usuario agregando la información directamente al contexto
        _context.Users.Add(new User
        {
            UserName = request.UserName,
            Password = hashedPassword,
            Email = request.Email,
            Status = EntityStatus.Unverified
        });
        await _context.SaveChangesAsync();

        // Devuelve una respuesta 201 Created
        return StatusCode(201);
    }

    // Método para encriptar la contraseña usando BCrypt
    private string HashPassword(string password)
    {
        // Genera una sal (string aleatorio para evitar duplicados, y mayor seguridad) y luego encripta la contraseña usando BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Representa una solicitud de registro de usuario y los parámetros que se solicitan
    public class RegistrationRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
