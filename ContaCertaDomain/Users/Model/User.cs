namespace ContaCerta.Domain.Users.Model
{
    public class User
    {
        public User()
        {
            Id = -1;
            Email = "";
            Password = "";
            LastAccess = null;
            Active = false;
            UserCosts = [];
        }

        public User(string email, string password, bool active)
        {
            Id = 0;
            Email = email;
            Password = password;
            LastAccess = DateTimeOffset.Now;
            Active = active;
            UserCosts = [];
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? LastAccess { get; set; }
        public bool Active { get; set; }
        public List<UserCost> UserCosts { get; set; } 
    }
}