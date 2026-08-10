// AuthController.cs — debug logging removed now that we've confirmed the backend works
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { error = "Username and password are required." });

            var validUsername  = _config["AdminCredentials:Username"];
            var hashedPassword = _config["AdminCredentials:HashedPassword"];

            bool usernameOk = string.Equals(request.Username.Trim(), validUsername, StringComparison.OrdinalIgnoreCase);
            bool passwordOk = !string.IsNullOrEmpty(hashedPassword) && BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword);

            if (!usernameOk || !passwordOk)
                return Unauthorized(new { error = "Invalid username or password." });

            var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(12);

            var token = new JwtSecurityToken(
                issuer:             _config["Jwt:Issuer"],
                audience:           _config["Jwt:Audience"],
                claims:             [new Claim(ClaimTypes.Name, request.Username)],
                expires:            expires,
                signingCredentials: creds
            );

            return Ok(new
            {
                token   = new JwtSecurityTokenHandler().WriteToken(token),
                expires = expires
            });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}