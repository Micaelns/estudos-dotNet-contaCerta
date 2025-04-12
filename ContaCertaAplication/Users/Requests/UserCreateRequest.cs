namespace ContaCerta.Aplication.Users.Requests;

public class UserCreateRequest
{
    public string Email {  get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Nickname { get; set; } = null;
    public bool IsPublicEmail { get; set; } = true;
}
