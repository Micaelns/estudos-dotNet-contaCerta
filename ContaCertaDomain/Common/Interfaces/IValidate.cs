namespace ContaCerta.Domain.Common.Interfaces
{
    public interface IValidate<T>
    {
        IReadOnlyList<string> Messages { get; }
        public bool IsValid(T element);
    }
}