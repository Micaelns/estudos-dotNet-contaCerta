namespace ContaCerta.Aplication.Common;

public class ResponseSingle<T>:Response
{
    public T? Data { get; set; } = default;
}
