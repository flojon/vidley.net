using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vidley.net.Data;
using vidley.net.Features.Users;

namespace vidley.net.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private IRepository<User> _userRepository { get; }
        private JwtSettings _jwtSettings { get; }


        public AuthController(IRepository<User> userRepository, IOptions<Settings> settings)
        {
            this._userRepository = userRepository;
            this._jwtSettings = settings.Value.JwtSettings;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(AuthParams auth)
        {
            var user = await _userRepository.FindOne(u => u.Email == auth.Email);
            if (user == null)
                return BadRequest("Login failed! Bad username or password");

            if (!BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
                return BadRequest("Login failed! Bad username or password");

            return Ok(user.GenerateJwtToken(_jwtSettings));
        }
    }
}