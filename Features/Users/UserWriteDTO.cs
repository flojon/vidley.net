namespace vidley.net.Features.Users
{
    public class UserWriteDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToUser()
        {
            return new User
            {
                Name = this.Name,
                Email = this.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(this.Password),
            };
        }
    }
}