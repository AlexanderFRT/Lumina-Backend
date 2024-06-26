﻿using Lumina_Backend.Data;
using Lumina_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(ApiDbContext context, TokenManager tokenManager) : ControllerBase
{
    private readonly ApiDbContext _context = context;
    private readonly TokenManager _tokenManager = tokenManager;

    // POST: api/Login https://localhost:7024/api/Login
    // Endpoint para iniciar sesión
    [HttpPost]
    public IActionResult PostLogin([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _context.Users.FirstOrDefault(u => u.UserName == request.UserName);
        if (user == null)
        {
            ModelState.AddModelError("UserName", "Nombre de usuario no encontrado.");
            return NotFound(ModelState);
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            ModelState.AddModelError("Password", "Contraseña incorrecta.");
            return Unauthorized(ModelState);
        }

        // Genera el token JWT
        var token = _tokenManager.GenerateToken(user);

        // Guarda el token generado para el usuario en el DB
        user.SessionToken = token;
        _context.SaveChanges();

        // Regresa el token y un mensaje de "Inicio de sesión exitoso"
        return Ok(new { Token = token, Message = "Inicio de sesión exitoso." });
    }

    // Representa una solicitud de inicio de sesión con credenciales de usuario
    public class LoginRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}