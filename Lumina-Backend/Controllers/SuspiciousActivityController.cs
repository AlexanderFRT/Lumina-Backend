using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lumina_Backend.Data;
using Lumina_Backend.Models;

namespace Lumina_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuspiciousActivityController : ControllerBase
{
    private readonly ApiDbContext _context;

}