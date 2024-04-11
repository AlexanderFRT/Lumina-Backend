/*using Lumina_Backend.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lumina_Backend.Models;
using Lumina_Backend.ModelsDTO;

 namespace Lumina_Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            Login loggedUser = await _userRepository.login(login);

            if (loggedUser != null)
            {
                return Ok(loggedUser);
            }
            else
                return BadRequest("User not found");
        }


        [HttpPost]
        public async Task<ActionResult> AddUser(Models.User user)
        {
            if (ModelState.IsValid)
            {
                Models.User newUser = new Models.User()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    SessionToken = user.SessionToken,
                    Email = user.Email,
                    FullName = user.FullName,
                    DateOfBirth = user.DateOfBirth,
                    Address = user.Address,
                    DNI = user.DNI
                };

                Models.User createdUser = await _userRepository.AddUser(newUser);

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
*/