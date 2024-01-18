Caso de usos:
** UC-US01 - Criar usuário [ContaCerta.Domain.Users.Services.CreateUser]:
	- VALIDATE US01 - Email válido;
	- VALIDATE US02 - Senha válida;
** UC-US02 - Listar usuários Ativos [ContaCerta.Domain.Users.Services.CreateUser]:
	- Listar todos os usuários ativos;


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
    