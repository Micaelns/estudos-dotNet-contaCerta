Caso de usos:
** UC-US01 - Criar usuário [ContaCerta.Domain.Users.Services.CreateUser]:
	- VALIDATE US01 - Email válido;
	- VALIDATE US02 - Senha válida;
	- Salvar Usuário;
** UC-US02 - Listar usuários Ativos [ContaCerta.Domain.Users.Services.CreateUser]:
	- Listar todos os usuários ativos;
** UC-CO01 - Criar Custo [ContaCerta.Domain.Costs.Services.CreateCost]:
	- VALIDATE CO02 - Custo valido;
	- Salvar Custo;
** UC-CO02 - Listar meus últimos Custo [ContaCerta.Domain.Costs.Services.MyLastCosts]:
	- Necessário enviar um Usuário ativo e quantos dias quer de histórico;
	- Se usuário inválido ou Inativo, retornar um erro;
	- A quantidade de dias não é obrigatório envio, mas se enviado deverá ser um numero inteiro maior que zero; 
	- Se não enviar a quantidade de dias, será considerado 15 dias;
	- Lista todos os Custos do usuário nos últimos dias;
** UC-CO03 - Listar meus próximos Custo [ContaCerta.Domain.Costs.Services.MyNextCosts]:
	- Necessário enviar um Usuário ativo;
	- Lista todos os Custos do usuário agendados para um dia/horário maior que agora;

Validações:
** VALIDATE US01 - Email válido [ContaCerta.Domain.Users.Validates.EmailValidate]:
	- Deve satisfazer o formato de email;
	- Aplicado regex para validar o formato de email: ^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$
	
** VALIDATE US02 - Senha válida [ContaCerta.Domain.Users.Validates.PasswordValidate]:
	- Não nula e espaços em branco;
    - Deve ter mais de 8 caracteres;
    - Deve ter pelo menos uma letra minúscula;
    - Deve ter pelo menos uma letra maiúscula;
    - Deve ter pelo menos um número;
    - Deve ter pelo menos um caractere especial;

** VALIDATE CO02 - Custo valido [ContaCerta.Domain.Costs.Validates.CostValidate]:
    - O titulo do custo não pode ser vazio;
    - O usuário deve ser válido;
    - O valor não pode ser negativo;
    