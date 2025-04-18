namespace ContaCerta.Aplication.Costs.DTOs;

public class CostDTO
{
    public int Id {  get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public float Value {  get; set; } = float.MinValue;
    public DateTime? PaymentDate { get; set; } = null;
    public SummaryUserCost? Summary { get; set; } = null;
    public string? Owner { get; set; } = null;
    public bool Active { get; set; } = false;
}
