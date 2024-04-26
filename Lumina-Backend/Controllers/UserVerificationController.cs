using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Data;
using Lumina_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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

        var userAccounts = await _context.Accounts
        .Where(account => account.User != null && account.User.Id == userId.Id)
        .Select(account => new AccountView
        {
            AccountNumber = account.AccountNumber,
            Type = account.Type.ToString(),
            Balance = account.Balance
        })
        .ToListAsync();

        var userAccountResponse = new UserAccountResponse
        {
            User = userViewModel,
            Accounts = userAccounts
        };

        return Ok(userAccountResponse);
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

        /* Validar FullName: Debe tener al menos 2 palabras separadas, máximo 4
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
        } */

        int accountNumberSaving = GenerarNumeroCuenta();
        int accountNumberChecking = GenerarNumeroCuenta();

        var accountSaving = new Account
        {
            User = user,
            AccountNumber = accountNumberSaving,
            Type = AccountType.Savings,
            Balance = 0,
            Status = EntityStatus.Active
        };

        var accountChecking = new Account
        {
            User = user,
            AccountNumber = accountNumberChecking,
            Type = AccountType.Checking,
            Balance = 0,
            Status = EntityStatus.Active
        };

        _context.Accounts.Add(accountSaving);
        _context.Accounts.Add(accountChecking);

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

    public class AccountView
    {
        public int AccountNumber { get; set; }
        public string? Type { get; set; }
        public decimal Balance { get; set; }
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

    public class UserAccountResponse
    {
        public UserView? User { get; set; }
        public List<AccountView>? Accounts { get; set; }
    }

    private int GenerarNumeroCuenta()
    {
        Random random = new();
        int numeroCuenta = 0;

        // Generar el prefijo del número de cuenta (en este caso, el código oficial del banco)
        int prefijo = 116;

        // Se asegura de que el número de cuenta generado sea único
        while (true)
        {
            // Genera un nuevo número de cuenta aleatorio
            string numeroCuentaAleatorio = "";
            for (int i = 0; i < 7; i++)
            {
                numeroCuentaAleatorio += random.Next(0, 10).ToString();
            }

            // Combina el prefijo del banco con los 6 dígitos aleatorios adicionales para generar un número de cuenta único
            numeroCuenta = int.Parse(prefijo.ToString() + numeroCuentaAleatorio);

            if (!_context.Accounts.Any(a => a.AccountNumber == numeroCuenta))
            {
                break;
            }
        }

        return numeroCuenta;
    }
}
