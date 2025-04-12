namespace ContaCerta.Domain.Common;

public class BaseModel
{
    public int Id { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = null;
    public bool IsDeleted { get; set; } = false;
}
