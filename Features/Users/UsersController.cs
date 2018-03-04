using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Data;

namespace vidley.net.Features.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController: ControllerBase
    {
        private IRepository<User> _repository { get; }

        public UsersController(IRepository<User> repository)
        {
            this._repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Post(UserWriteDTO userParams)
        {
            var existingUser = await _repository.FindOne(u => u.Email == userParams.Email);
            if (existingUser != null)
                return BadRequest("There is already a user registered with the given email");

            var user = userParams.ToUser();
            await _repository.Add(user);

            return Ok(new UserReadDTO(user));
        }
    }
}
