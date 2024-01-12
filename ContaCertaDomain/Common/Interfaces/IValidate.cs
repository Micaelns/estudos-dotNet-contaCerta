namespace ContaCerta.Domain.Common.Interfaces
{
    public interface IValidate
    {
        IReadOnlyList<string> Messages { get; }
        public bool Execute(string Password);
    }
}