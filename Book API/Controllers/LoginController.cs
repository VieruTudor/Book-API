using Book_API.Model;
using BookAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Book_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult Login([FromBody] AuthRequest login)
        {
            IActionResult response = Unauthorized();
            var token = userService.Authenticate(login.UserName, login.Password);
            if (token != null)
                response = Ok(token);
            return response;
        }
    }
}
