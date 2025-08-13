namespace Vehicle_Configurator_Project.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Vehicle_Configurator_Project.IServices;
    using Vehicle_Configurator_Project.Models;
    using Vehicle_Configurator_Project.Token;

    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var createdUser = await _userService.RegisterAsync(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                var baseEx = ex;
                while (baseEx.InnerException != null) baseEx = baseEx.InnerException;
                return BadRequest(baseEx.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequst request)
        {
            try
            {
                // LoginAsync returns a JWT token string
                var token = await _userService.LoginAsync(request.Email, request.Password);

                // Get the full user details by email (UserId, Email)
                var user = await _userService.GetUserByEmailAsync(request.Email);

                // Return token along with userId and email
                return Ok(new
                {
                    token,
                    userId = user.UserId,  // UserId from your User model
                    email = user.Email
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
