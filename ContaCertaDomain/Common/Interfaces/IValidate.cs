namespace ContaCerta.Domain.Common.Interfaces
{
    public interface IValidate
    {
        IReadOnlyList<string> Messages { get; }
        public bool IsValid(string text);
    }
}