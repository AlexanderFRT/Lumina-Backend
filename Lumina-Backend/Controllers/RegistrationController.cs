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
    public async Task<IActionResult> PostUser([FromBody] RegistrationRequest request)
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

        // Validación para los datos del usuario
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Se requieren nombre de usuario, contraseña y correo electrónico.");
        }
        if (request.UserName.Length < 6 || request.UserName.Length > 16)
        {
            return BadRequest("El nombre de usuario debe tener entre 6 y 16 caracteres.");
        }
        if (request.Password.Length < 8 || request.Password.Length > 16)
        {
            return BadRequest("La contraseña debe tener entre 8 y 16 caracteres.");
        }
        if (!IsValidEmail(request.Email))
        {
            return BadRequest("Formato de correo electrónico inválido.");
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

    // Método auxiliar para validar el formato de correo electrónico
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
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