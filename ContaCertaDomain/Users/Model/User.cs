namespace ContaCerta.Domain.Users.Model;

public class User
{
    public int Id { get; set; } = 0;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset? LastAccess { get; set; } = DateTimeOffset.Now;
    public bool Active { get; set; } = false;
    //public List<UserCost> UserCosts { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        User user = (User)obj;
        return user.Email == Email && user.Active == Active;
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode() ^ Active.GetHashCode();
    }
}