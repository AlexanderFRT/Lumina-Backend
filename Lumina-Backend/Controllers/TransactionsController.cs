using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lumina_Backend.Data;
using Lumina_Backend.Models;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController(ApiDbContext context, ILogger<TransactionsController> logger) : ControllerBase
{
    private readonly ApiDbContext _context = context;
    private readonly ILogger<TransactionsController> _logger = logger;

    [HttpPost("Transferir")]
    public async Task<IActionResult> TransferMoney([FromBody] TransferRequest transferRequest)
    {
        try
        {
            var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferRequest.SenderAccountNumber);
            var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferRequest.ReceiverAccountNumber);

            if (senderAccount == null || receiverAccount == null)
            {
                return NotFound("One or both of the accounts were not found.");
            }

            if (senderAccount.Balance < transferRequest.Amount)
            {
                return BadRequest("Insufficient funds in the sender's account.");
            }

            senderAccount.Balance -= transferRequest.Amount;

            receiverAccount.Balance += transferRequest.Amount;

            var transaction = new Transaction
            {
                AccountNumber = senderAccount.AccountNumber,
                Account = senderAccount,
                Type = TransactionType.Transfer,
                Amount = transferRequest.Amount,
                TransactionDescription = "Transferencia realizada a la cuenta " + receiverAccount.AccountNumber
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
