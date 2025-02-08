using ContaCerta.Domain.Costs.Model;

namespace ContaCerta.Domain.Users.Model;

public class UserCost
{
    public int Id { get; set; } = 0;
    public User User { get; set; } = new();
    public Cost Cost { get; set; } = new();
    public float Value { get; set; } = 0;
    public bool Paid { get; set; } = false;
    public DateTime? Paid_at { get; set; } = null;
    
}
