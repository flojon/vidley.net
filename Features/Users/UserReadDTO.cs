namespace vidley.net.Features.Users
{
    public class UserReadDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public UserReadDTO(User user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
        }
    }
}