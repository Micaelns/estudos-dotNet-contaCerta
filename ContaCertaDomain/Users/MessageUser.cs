namespace ContaCerta.Domain.Users;

public class MessageUser
{
    public static string ErrorFind { get; } = "Erro ao consultar usuário";
    public static string ErrorSave { get; } = "Erro ao salvar usuario";
    public static string InvalidUser { get; } = "Usuário inválido ou inativo!";
    public static string InvalidEmail { get; } = "E-mail inválido!";
    public static string UserExists { get; } = "Email cadastrado anteriormente!";
    public static string InvalidPasswordEmpty { get; } = "A senha não pode ser vazia.";
    public static string InvalidPasswordSort { get; } = "A senha deve ter mais de {0} caracteres.";
    public static string InvalidPasswordNoLowerCase { get; } = "A senha deve ter pelo menos uma letra minúscula.";
    public static string InvalidPasswordNoUpperCase { get; } = "A senha deve ter pelo menos uma letra maiúscula.";
    public static string InvalidPasswordNoNumbers { get; } = "A senha deve ter pelo menos um número.";

}
