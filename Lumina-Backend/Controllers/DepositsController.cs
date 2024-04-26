using Lumina_Backend.Data;
using Lumina_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepositsController(ApiDbContext context, ILogger<DepositsController> logger) : MainController
{
    private readonly ApiDbContext _context = context;
    private readonly ILogger<DepositsController> _logger = logger;

    [HttpGet("ListaDeCuentas")]
    public async Task<ActionResult<IEnumerable<Account>>> GetUserAccounts()
    {
        try
        {
            var userId = GetAuthenticatedUserId();
            var user = await _context.Users.Include(u => u.Accounts)
                                            .FirstOrDefaultAsync(u => u.Id == userId.Id);

            if (user == null)
            {
                return Unauthorized("ID de usuario no encontrado en el token JWT.");
            }

            var accountInfo = user?.Accounts?.Select(a => new
            {
                a.Id,
                a.AccountNumber,
                a.Type,
                a.Balance
            }).ToList();

            return Ok(accountInfo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar las cuentas del usuario.");

            return StatusCode(500, "Se produjo un error al procesar la solicitud.");
        }
    }

    [HttpPost("DepositarSaldo")]
    public async Task<ActionResult<Transaction>> MakeDeposit([FromBody] DepositRequest depositRequest)
    {
        try
        {
            var userId = GetAuthenticatedUserId();
            var user = await _context.Users.Include(u => u.Accounts)
                                            .FirstOrDefaultAsync(u => u.Id == userId.Id);

            if (user == null)
            {
                return Unauthorized("ID de usuario no encontrado en el token JWT.");
            }

            if (user.Accounts != null)
            {
                var frozenAccount = user.Accounts.FirstOrDefault(a => a.Status == EntityStatus.Frozen);
                if (frozenAccount != null)
                {
                    return BadRequest("La cuenta del usuario está congelada y no se pueden realizar transacciones.");
                }
            }
            else
            {
                return BadRequest("La colección de cuentas del usuario es nula.");
            }

            var account = user?.Accounts?.FirstOrDefault(a => a.AccountNumber == depositRequest.AccountNumber);

            if (account == null)
            {
                return BadRequest("La cuenta especificada no pertenece al usuario autenticado.");
            }

            if (depositRequest.Amount <= 0)
            {
                return BadRequest("El monto del depósito debe ser mayor que cero.");
            }

            var depositTransaction = new Transaction
            {
                AccountNumber = account.AccountNumber,
                Account = account,
                Type = TransactionType.Deposit,
                Amount = depositRequest.Amount,
                TransactionDescription = "Depósito realizado",
                Status = EntityStatus.Completed
            };

            account.Balance += depositRequest.Amount;

            _context.Transactions.Add(depositTransaction);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Depósito realizado exitosamente." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al depositar los fondos.");

            return StatusCode(500, "Se produjo un error al procesar la solicitud.");
        }
    }

    public class DepositRequest
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
    }
}