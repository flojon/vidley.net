using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using vidley.net.Data;
using vidley.net.Features.Users;

namespace vidley.net.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private IRepository<User> _userRepository { get; }

        public AuthController(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(AuthParams auth)
        {
            var user = await _userRepository.FindOne(u => u.Email == auth.Email);
            if (user == null)
                return BadRequest("Login failed! Bad username or password");

            if (!BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
                return BadRequest("Login failed! Bad username or password");
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key to encrypt jwt tokens"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);



            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}