namespace ContaCerta.Aplication.Common;

public class Response
{
    public StatusCode Status { get; set; } = StatusCode.Success;
    public string StatusMessage { get; set; } = string.Empty;
}
