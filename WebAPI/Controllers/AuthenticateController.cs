using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTManagerRepository tokenRepository;
        private readonly IDatabaseHelper databaseHelper;
        private readonly IMapper mapper;
        public AuthenticateController(IJWTManagerRepository jWTManagerRepository, IDatabaseHelper databaseHelper, IMapper mapper)
        {
            this.tokenRepository = jWTManagerRepository;
            this.databaseHelper = databaseHelper;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest user)
        {
            var token = tokenRepository.Authenticate(user.Email, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { data = token, status = true, code = 200, message = "Login Successfully", meta = new {token = token.Token } });
        }

        [HttpPost]
        [Route("register")]
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

        [HttpGet]
        [Route("user")]
        [Authorize]
        public IActionResult GetUser()
        {
            var id = User.FindFirst(ClaimTypes.PrimarySid).Value;
            var user = databaseHelper.Users.Find(id);
            if (user != null)
            {
                var userDto = mapper.Map<User, UserDto>(user);
                
                return Ok(new { status = true, data = userDto, code = 200 });
            }

            return Unauthorized();
        }
    }
}
