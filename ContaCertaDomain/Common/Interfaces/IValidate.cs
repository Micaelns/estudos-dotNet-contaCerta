namespace ContaCerta.Domain.Common.Interfaces;

public interface IValidate<T>
{
    public string ErrorMessages { get; }
    public bool IsValid(T element);
}