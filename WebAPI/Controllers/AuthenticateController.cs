using Library.Common.Dtos;
using Library.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTManagerRepository tokenRepository;
        private readonly IDatabaseHelper databaseHelper;
        public AuthenticateController(IJWTManagerRepository jWTManagerRepository, IDatabaseHelper databaseHelper)
        {
            this.tokenRepository = jWTManagerRepository;
            this.databaseHelper = databaseHelper;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest user)
        {
            var token = tokenRepository.Authenticate(user.Email, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User user)
        {
            var response = new ServiceResponse<User>();
            var data = this.databaseHelper.Users.Create(user);
            if (data != null)
            {
                response.SetSuccess(data);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
