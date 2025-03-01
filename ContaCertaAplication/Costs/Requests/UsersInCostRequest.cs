namespace ContaCerta.Aplication.Costs.Requests;

public class UsersInCostRequest
{
    public string[] NewEmails { get; set; } = [];
    public string[] RemoveEmails { get; set; } = [];
}
