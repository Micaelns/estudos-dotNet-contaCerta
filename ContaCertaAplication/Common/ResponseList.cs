namespace ContaCerta.Aplication.Common;

public class ResponseList<T>:Response
{
    public IEnumerable<T>? Data { get; set; } = null;
    public Pagination? Pagination { get; set; } = null;
}
