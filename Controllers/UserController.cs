using Well_Up_API.Services;
using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;
namespace Well_Up_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserRequest user)
        {
            var newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            var id = _userService.Register(newUser);

            return CreatedAtAction(nameof(Register), id);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            var userResponse = _userService.Login(user);
            if (userResponse == null)
            {
                return NotFound();
            }
            return Ok(userResponse);
        }
    }
}