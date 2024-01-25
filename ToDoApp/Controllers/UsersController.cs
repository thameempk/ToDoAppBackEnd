using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly IConfiguration _configuration;

        public UsersController(IUsers users, IConfiguration configuration)
        {
            _configuration = configuration;
            _users = users;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(_users.GetUsers());
        }

        [HttpPost("Register")]

        public ActionResult Register([FromBody ] RegisterData register)
        {
            if (register == null)
            {
                return BadRequest();
            }

            var userExist = _users.GetUsers().FirstOrDefault(s => s.email == register.email && s.password == register.password);
            if (userExist != null)
            {
                return BadRequest("email or password already exist");
            }

            _users.Register(register);
            return Ok();

        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Login login )
        {
            if (login == null)
            {
                return BadRequest();
            }
            var user = _users.Login(login);
            if (user == null)
            {
                return BadRequest("email or password incorrect");
            }

            string token = GenerateJwtToken(user);
            return Ok(new { Token = token, name = user.name, userId = user.userId });


        }

        private string GenerateJwtToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Email, user.email),
        };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)

            ); ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
