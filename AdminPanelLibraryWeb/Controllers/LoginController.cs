using AdminPanelLibraryWeb.Models;
using LibraryWebApi.Data;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace AdminPanelLibraryWeb.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DbHelper<LibraryContext> _db;

        public LoginController(IConfiguration configuration, LibraryContext dbContext)
        {
            _config = configuration;
            _db = new DbHelper<LibraryContext>(dbContext);
        }

        [HttpPost]
        [Route("getpassword/")]
        public IActionResult GetPassword([FromBody] string name)
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetUserPassword(name), _db.GetUserPassword(name));
            return (response.Code == "1" || response == null) ? BadRequest(response) : Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);
            if(user != null)
            {
                var response = ResponseHandler.HandleDbOperations(() => GenerateToken(user), GenerateToken(user));
                return (response.Code == "1" || response == null) ? BadRequest(response) : Ok(response);
            }
            return NotFound("User not found");
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt")["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.GivenName, user.Login),
                new Claim(ClaimTypes.Role, user.Role.Rolename)
            };

            var token = new JwtSecurityToken(_config.GetSection("Jwt")["Issuer"],
                _config.GetSection("Jwt")["Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(UserLogin userLogin)
        {
            return await _db.GetUserAuthAsync(userLogin.Login, userLogin.Password);
        }
    }
}
