namespace ContaCertaApi.Domains.Interfaces
{
    public interface IValidate
    {
        IReadOnlyList<string> Messages { get; }
        public bool Execute(string Password);
    }
}