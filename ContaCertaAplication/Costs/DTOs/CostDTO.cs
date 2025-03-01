namespace ContaCerta.Aplication.Costs.DTOs;

public class CostDTO
{
    public int Id {  get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public float Value {  get; set; } = float.MinValue;
    public DateTime? PaymentDate { get; set; } = DateTime.MinValue;
    public bool Active { get; set; } = false;
}
