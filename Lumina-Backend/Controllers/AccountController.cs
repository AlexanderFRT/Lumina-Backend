using Lumina_Backend.Data;
using Lumina_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using static Lumina_Backend.Models.Account;


namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ApiDbContext _context;
    
    public AccountController(ApiDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount([FromBody] AccountRequest request)
    {
        
        string accountNumberGenerated = GenerarNumeroCuenta();

        var account = new Account
        {
            AccountNumber = accountNumberGenerated,
            Type = request.Type,
            Balance = 0
        };

        _context.Accounts.Add(account);

        return StatusCode(201);

    }
    
    [HttpGet]
    public async Task<ActionResult<Account>> GetAccountTransactions(string accountNumber)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account == null)
        {
            return NotFound();
        }       
        
        return account;
    }

    public class AccountRequest
    {
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
    }
    
    public string GenerarNumeroCuenta()
    {
        Random random = new Random();
        string numeroCuenta = "";

        // Generar el prefijo del número de cuenta (por ejemplo, el código del banco)
        string prefijo = "1234"; // Ejemplo de prefijo

        // Generar el número de cuenta aleatorio de longitud 
        for (int i = 0; i < 8; i++)
        {
            numeroCuenta += random.Next(0, 10).ToString();
        }

        // Combinar el prefijo con el número de cuenta aleatorio
        numeroCuenta = prefijo + numeroCuenta;

        if (_context.Accounts.Any(a => a.AccountNumber == numeroCuenta))
        {
            return GenerarNumeroCuenta();
        }

        return numeroCuenta;
    }
}

