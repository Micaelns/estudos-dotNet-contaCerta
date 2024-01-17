namespace ContaCerta.Domain.Users.Model
{
    public class User
    {
        public User() {
            Email = "";
            Password = "";
            LastAccess = null;
            Active = false;
        }

        public User(string email, string password, bool active)
        {
            Email = email;
            Password = password;
            LastAccess = DateTimeOffset.Now;
            Active = active;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? LastAccess { get; set; }
        public bool Active { get; set; }
    }
}