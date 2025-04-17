using ContaCerta.Aplication.Costs.DTOs;

namespace ContaCerta.Aplication.Users.DTOs;

public class UserCostDTO
{
    public CostDTO? Cost { get; set; } = null;
    public float Value { get; set; } = float.MinValue;
    public DateTime? Paid_at { get; set; } = DateTime.MinValue;
    public bool Paid { get; set; } = false;
}
