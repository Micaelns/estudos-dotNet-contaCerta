namespace ContaCerta.Domain.Costs;

public class MessageCost
{
    public static string ErrorSave { get; } = "Erro ao salvar custo";
    public static string ErrorLastDaysQuery { get; } = "Erro na consulta de custos nos ultimos {0} dias criado por {1}.";
    public static string ErrorNextDaysQuery { get; } = "Erro na consulta dos próximos custos criado por {0}.";
    public static string ImpossibleManagerUsersIfAnyPaid { get; } = "Após algum pagamento não é possível gerenciar usuários!";
    public static string InvalidCost { get; } = "Custo inválido ou inativo!"; 
    public static string InvalidNumberOfDays { get; } = "O número de dias deve ser maior ou igual a zero!";
    public static string TitleCanNotEmpty { get; } = "O titulo do custo não pode ser vazio.";
    public static string ValueCanNotNegative { get; } = "O valor não pode ser negativo.";
    public static string UserCanNotInvalid { get; } = "O usuário deve ser válido.";
}
