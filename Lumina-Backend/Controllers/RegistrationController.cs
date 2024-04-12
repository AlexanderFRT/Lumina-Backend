using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Data;
using Lumina_Backend.Models;

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
    public async Task<ActionResult<User>> PostUser([FromBody] RegistrationRequest request)
    {
        // Chequea si el nombre de usuario está tomado
        if (_context.Users.Any(u => u.UserName == request.UserName))
        {
            return BadRequest("El nombre de usuario ya está en uso.");
        }

        // Chequea si el email ya está registrado
        if (_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest("El correo electrónico ya está registrado.");
        }

        // Construir el objeto de usuario a partir de los parámetros de la solicitud
        var user = new User
        {
            UserName = request.UserName,
            Password = request.Password,
            Email = request.Email
        };

        // Validación para los datos del usuario
        if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("Se requieren nombre de usuario, contraseña y correo electrónico.");
        }
        if (!IsValidEmail(user.Email))
        {
            return BadRequest("Formato de correo electrónico inválido.");
        }
        if (user.UserName.Length < 6 || user.UserName.Length > 16)
        {
            return BadRequest("El nombre de usuario debe tener entre 6 y 16 caracteres.");
        }
        if (user.Password.Length < 8 || user.Password.Length > 16)
        {
            return BadRequest("La contraseña debe tener entre 8 y 16 caracteres.");
        }

        // Encriptar la contraseña antes de almacenarla en la base de datos por motivos de seguridad
        user.Password = HashPassword(user.Password);

        // Agregando el usuario a la base de datos
        _context.Users.Add(user);
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