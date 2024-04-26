using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lumina_Backend.Data;
using Lumina_Backend.Models;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuspiciousActivityController(ApiDbContext context, ILogger<SuspiciousActivityController> logger) : MainController
{
    private readonly ApiDbContext _context = context;
    private readonly ILogger<SuspiciousActivityController> _logger = logger;

    [HttpPost("CheckSuspiciousActivity")]
    public async Task<IActionResult> CheckSuspiciousActivity([FromBody] SuspiciousActivityRequest request)
    {
        try
        {
            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == request.AccountId);

            if (account == null)
            {
                return NotFound("Cuenta no encontrada.");
            }

            var transfersWithinHour = await _context.Transactions
                .Where(t => t.Account.Id == request.AccountId && t.DateAdded >= DateTime.UtcNow.AddHours(-1))
                .CountAsync();

            if (transfersWithinHour >= 5)
            {
                account.Status = EntityStatus.Frozen;
                await _context.SaveChangesAsync();

                if (account.User != null)
                {
                    LogSuspiciousActivity(account.User, ActionType.Transaction, LogStatus.Flagged);
                }
                else
                {
                    _logger.LogError("El usuario asociado a la cuenta es nulo.");
                }

                return Ok(new { Message = "Cuenta congelada por actividad sospechosa." });
            }
            else
            {
                if (account.User != null)
                {
                    LogSuspiciousActivity(account.User, ActionType.Transaction, LogStatus.Ok);
                }
                else
                {
                    _logger.LogError("El usuario asociado a la cuenta es nulo.");
                }
            }

            return Ok(new { Message = "No se ha detectado ninguna actividad sospechosa." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar los cambios");

            return StatusCode(500, "Se ha producido un error al comprobar una actividad sospechosa.");
        }
    }

    public class SuspiciousActivityRequest
    {
        public int AccountId { get; set; }
    }

    private void LogSuspiciousActivity(User user, ActionType actionType, LogStatus status)
    {
        var log = new Log
        {
            User = user,
            Action = actionType,
            Status = status,
            Timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
    };

        _context.Logs.Add(log);
        _context.SaveChanges();
    }
}