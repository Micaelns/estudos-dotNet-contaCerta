namespace ContaCerta.Aplication.Users.DTOs;

public class UserDTO
{
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = false;
}
