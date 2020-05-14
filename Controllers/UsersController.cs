using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreApiWithJwtTokens.Services;
using CoreApiWithJwtTokens.Models;
using System.Linq;

namespace CoreApiWithJwtTokens.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = UserService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = UserService.GetAll();
            return Ok(users);
        }
    }
}