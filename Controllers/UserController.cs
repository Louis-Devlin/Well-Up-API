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

            if (id == -1)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(Register), id);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserRequest user)
        {
            var details = new User()
            {
                Email = user.Email,
                Password = user.Password
            };

            var userResponse = _userService.Login(details);
            if (userResponse == null)
            {
                return NotFound();
            }
            return Ok(userResponse);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UserRequest user)
        {
            var result = _userService.Update(id, user);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}