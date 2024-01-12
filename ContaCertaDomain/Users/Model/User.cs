namespace ContaCerta.Domains.Users.Model
{
    public class User
    {
        public User() { }
        public User(string email, string password, bool active)
        {
            Email = email;
            Password = password;
            DateTimeOffset LastAccess = DateTimeOffset.Now;
            Active = active;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? LastAccess { get; set; }
        public bool Active { get; set; }
    }
}