using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Data;
using Lumina_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserVerificationController(ApiDbContext context) : MainController
{
    private readonly ApiDbContext _context = context;

    [HttpGet("DatosUsuario")]
    public async Task<ActionResult<UserView>> GetUser()
    {
        var userId = GetAuthenticatedUserId();
        var user = await _context.Users.FindAsync(userId.Id);

        if (user == null)
        {
            return NotFound();
        }

        var userViewModel = new UserView
        {
            UserName = user.UserName,
            Email = user.Email,
            FullName = user.FullName,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address,
            DNI = user.DNI,
            ProfileImage = user.ProfileImage,
            Status = user.Status
        };

        return userViewModel;
    }

    [HttpPut("VerificaciónUsuario")]
    public async Task<IActionResult> UpdateUserDetails(UserVerificationRequest userDetails)
    {
        var userId = GetAuthenticatedUserId();
        var user = await _context.Users.FindAsync(userId.Id);

        if (user == null)
        {
            return NotFound();
        }

        if (user.Status == EntityStatus.Verified)
        {
            return BadRequest("El usuario ya está verificado.");
        }

        user.FullName = userDetails.FullName?.Trim();
        user.DateOfBirth = userDetails.DateOfBirth;
        user.Address = userDetails.Address?.Trim();
        user.DNI = userDetails.DNI?.Trim();

        // Validar FullName: Debe tener al menos 2 palabras separadas, máximo 4
        if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length < 2 || user.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length > 4)
        {
            return BadRequest("El nombre completo debe tener al menos 2 palabras y como máximo 4.");
        }

        // Validar Address: Debe tener al menos 30 caracteres
        if (string.IsNullOrWhiteSpace(user.Address) || user.Address.Length < 20 || user.Address.Length > 80)
        {
            return BadRequest("La dirección debe tener al menos 20 carácteres, y no más de 80.");
        }

        // Validar DNI: Debe tener entre 6 y 12 dígitos
        if (string.IsNullOrWhiteSpace(user.DNI) || user.DNI.Length < 6 || user.DNI.Length > 12 || !user.DNI.All(char.IsDigit))
        {
            return BadRequest("El DNI debe tener entre 6 y 12 dígitos numéricos.");
        }

        // Validar DateOfBirth: Asegurar que todas las partes (día, mes, año) estén completas
        if (!user.DateOfBirth.HasValue || user.DateOfBirth.Value == default)
        {
            return BadRequest("La fecha de nacimiento es obligatoria.");
        }

        user.Status = EntityStatus.Verified;

        await _context.SaveChangesAsync();

        return Ok(new { Message = "Datos del usuario actualizados correctamente." });
    }

    public class UserView
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? DNI { get; set; }
        public string? ProfileImage { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class UserVerificationRequest
    {
        public string? FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? DNI { get; set; }
    }
}
