namespace ContaCerta.Aplication.Costs.Requests;

public class CostCreateRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public float Value { get; set; } = float.MinValue;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime? PaymentDate { get; set; } = null;
}
