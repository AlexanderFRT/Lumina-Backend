using Lumina_BackEnd.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lumina_BackEnd.Models;

namespace Lumina_BackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    SessionToken = user.SessionToken,
                    Email = user.Email,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Address = user.Address,
                    DNI = user.DNI,
                    Accounts = user.Accounts,
                    Securities = user.Securities,
                    Logs = user.Logs,
                };

                User createdUser = await _userRepository.AddUser(newUser);

                if (createdUser != null)
                {
                    return (Ok("User added Succesfully"));
                }
                else
                    return BadRequest("User could not be added");

            }
            else
                return BadRequest("User is invalid");
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userRepository.GetUser(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.GetUsers());
        }

    }
}
