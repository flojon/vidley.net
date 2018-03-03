using System.ComponentModel.DataAnnotations;

namespace vidley.net.Features.Auth
{
    public class AuthParams
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}