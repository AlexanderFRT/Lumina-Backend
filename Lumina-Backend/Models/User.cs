using System.ComponentModel.DataAnnotations;

namespace Lumina_Backend.Models;

public class User : BaseEntity
{
    [StringLength(16, MinimumLength = 6, ErrorMessage = "El nombre de usuario debe tener entre 6 y 16 caracteres.")]
    public string UserName { get; set; }

    [StringLength(16, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres.")]
    public string Password { get; set; }

    [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
    public string Email { get; set; }

    public string? SessionToken { get; set; }

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? DNI { get; set; }

    public string? ProfileImage { get; set; }

    public ICollection<Account> Accounts { get; set; }
    public ICollection<Security> Securities { get; set; }
    public ICollection<Log> Logs { get; set; }
}