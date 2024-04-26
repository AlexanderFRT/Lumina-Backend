using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lumina_Backend.Data;
using Lumina_Backend.Models;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController(ApiDbContext context, ILogger<TransactionsController> logger) : MainController
{
    private readonly ApiDbContext _context = context;
    private readonly ILogger<TransactionsController> _logger = logger;

    [HttpPost("Transferir")]
    public async Task<IActionResult> TransferMoney([FromBody] TransferRequest transferRequest)
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

            var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferRequest.SenderAccountNumber);
            var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferRequest.ReceiverAccountNumber);

            if (senderAccount == null || receiverAccount == null)
            {
                return NotFound("Una o ambas cuentas no fueron encontradas.");
            }

            if (senderAccount.Balance < transferRequest.Amount)
            {
                return BadRequest("Fondos insuficientes en la cuenta de envió.");
            }

            senderAccount.Balance -= transferRequest.Amount;

            receiverAccount.Balance += transferRequest.Amount;

            var transaction = new Transaction
            {
                AccountNumber = senderAccount.AccountNumber,
                Account = senderAccount,
                Type = TransactionType.Transfer,
                Amount = transferRequest.Amount,
                TransactionDescription = "Transferencia realizada a la cuenta " + receiverAccount.AccountNumber,
                Status = EntityStatus.Completed
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Transferencia realizada exitosamente." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al realizar la transferencia de fondos.");
            return StatusCode(500, "Se produjo un error al procesar la solicitud.");
        }
    }
    public class TransferRequest
    {
        public int SenderAccountNumber { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
