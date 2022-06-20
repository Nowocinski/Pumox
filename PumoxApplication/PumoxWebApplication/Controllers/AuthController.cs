using Microsoft.AspNetCore.Mvc;
using PumoxWebApplication.Repositories;

namespace PumoxWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtHandler _jwtHandler;
        public AuthController(ILogger<AuthController> logger, IJwtHandler jwtHandler)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Login")]
        public async Task<object> Login(string login, string password)
        {
            if (login != "admin" || password != "admin")
            {
                return NotFound("User not found");
            }

            string token = _jwtHandler.CreateToken(login, password);

            return new
            {
                Token = token
            };
        }
    }
}
