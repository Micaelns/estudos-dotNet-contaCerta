using ContaCerta.Domain.Common;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Model;

public class Cost: BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public float Value { get; set; } = 0;
    public DateTime? PaymentDate { get; set; }
    public bool Active { get; set; } = false;
    public User UserRequested { get; set; } = new();
    //public List<UserCost> UserCosts { get; set; }

}