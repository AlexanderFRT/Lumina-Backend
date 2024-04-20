using Lumina_Backend.Data;
using Lumina_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;


namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ApiDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("CuentaAhorro")]
    public async Task<ActionResult<Account>> CreateSavingsAccount([FromBody] AccountRequest request)
    {
        try
        {
            var userId = GetAuthenticatedUserId();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Id);

            if (user == null)
            {
                return Unauthorized("ID de usuario no encontrado en el token JWT.");
            }

            // Genera un nuevo número de cuenta.
            int accountNumberGenerated = GenerarNumeroCuenta();

            // Crea la nueva cuenta asociada al usuario.
            var account = new Account
            {
                User = user,
                AccountNumber = accountNumberGenerated,
                Type = AccountType.Savings,
                Balance = 0,
                Status = EntityStatus.Active
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            // Devuelve una respuesta indicando que se creó la cuenta satisfactoriamente.
            return CreatedAtAction(nameof(GetAccountTransactions), new { accountNumber = accountNumberGenerated }, account);
        }
        catch (Exception ex)
        {
            // Registra la excepción en el registro de errores
            _logger.LogError(ex, "Error creating account.");

            // Maneja las excepciones de manera adecuada y devuelve un mensaje de error genérico.
            return StatusCode(500, "Se produjo un error al procesar la solicitud.");
        }
    }

    [HttpPost("CuentaCorriente")]
    public async Task<ActionResult<Account>> CreateCheckingAccount([FromBody] AccountRequest request)
    {
        try
        {
            var userId = GetAuthenticatedUserId();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Id);

            if (user == null)
            {
                return Unauthorized("ID de usuario no encontrado en el token JWT.");
            }

            int accountNumberGenerated = GenerarNumeroCuenta();

            var account = new Account
            {
                User = user,
                AccountNumber = accountNumberGenerated,
                Type = AccountType.Checking,
                Balance = 0,
                Status = EntityStatus.Active
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccountTransactions), new { accountNumber = accountNumberGenerated }, account);
        }
        catch (Exception)
        {
            return StatusCode(500, "Se produjo un error al procesar la solicitud.");
        }
    }
    
    [HttpGet("HistorialDeTransacciones")]
    public async Task<ActionResult<Account>> GetAccountTransactions(int accountNumber)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null)
        {
            return NotFound();
        }       
        
        return account;
    }

    private int GenerarNumeroCuenta()
    {
        Random random = new Random();
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

    private UserId GetAuthenticatedUserId()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("El token JWT no fue encontrado en la solicitud.");
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null)
        {
            throw new InvalidOperationException("El token JWT no es válido.");
        }

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

    public class UserId
    {
        public int Id { get; set; }
    }

    public class AccountRequest
    {
        public AccountType Type { get; set; }
    }
}

