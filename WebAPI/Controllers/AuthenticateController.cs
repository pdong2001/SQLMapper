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

            return Ok(new { data = token, code = 200, message = "Login Successfully" });
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
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var users = databaseHelper.Users.GetList(1, new DbQueryParameter
            {
                Name = "Email",
                Value = email,
                CompareOperator = CompareOperator.Equal,
            });
            if (users.Any())
            {
                var user = users.First();
                return Ok(mapper.Map<User, UserDto>(user));
            }

            return Unauthorized();
        }
    }
}
