Caso de usos:
===================================

**UC-US01** - *Criar usuário [**ContaCerta.Domain.Users.Services.CreateUser**]*
- **VALIDATE_US01** - *Email válido*
- **VALIDATE_US02** - *Senha válida*
- Salvar Usuário

**UC-US02** - *Listar usuários Ativos [ContaCerta.Domain.Users.Services.CreateUser]*
- Listar todos os usuários ativos;

**UC-CO01** - *Criar Custo [ContaCerta.Domain.Costs.Services.CreateCost]*
- **VALIDATE_CO01** - *Custo válido*
- Salvar Custo

**UC-CO02** - *Listar últimos Custos de um usuário [ContaCerta.Domain.Costs.Services.LastCosts]*
- Necessário enviar um Usuário ativo e quantos dias quer de histórico
- Se usuário inválido ou Inativo, retorna um erro
- A quantidade de dias não é obrigatório envio, mas se enviado deverá ser um número inteiro maior que zero
- Se não enviar a quantidade de dias, será considerado 15 dias
- Lista todos os Custos do usuário nos últimos x dias

**UC-CO03** - *Listar próximos Custo de um usuário [ContaCerta.Domain.Costs.Services.NextCosts]*
- Necessário enviar um Usuário ativo
- Lista todos os Custos do usuário agendados para um dia/horário maior que agora

**UC-CO04** - *Listar últimos Custos criado por um usuário [ContaCerta.Domain.Costs.Services.LastCostsCreatedByUser]*
- Necessário enviar um Usuário ativo e quantos dias quer de histórico
- Se usuário inválido ou Inativo, retorna um erro
- A quantidade de dias não é obrigatório envio, mas se enviado deverá ser um número inteiro maior que zero
- Se não enviar a quantidade de dias, será considerado 15 dias
- Lista todos os Custos cadastrados por usuário informado nos últimos x dias

**UC-CO05** - *Listar próximos Custo criado por um usuário [ContaCerta.Domain.Costs.Services.NextCostsCreatedByUser]*
- Necessário enviar um Usuário ativo
- Lista todos os Custos do usuário agendados por usuário informado para um dia/horário maior que agora

**UC-CO06** - *Listar últimos Custos de um usuário [ContaCerta.Domain.Costs.Services.LastCostsNoPay]*
- Necessário enviar um Usuário válido
- Se usuário inválido, retorna um erro
- Lista todos os Custos do usuário informado que não foi pago e já devia ter sido pago

<br/><br/>

Validações:
===============

**VALIDATE_US01** - *Email válido [ContaCerta.Domain.Users.Validates.EmailValidate]*
- Deve satisfazer o formato de email
- Aplicado regex para validar o formato de email: ``^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$``
	
**VALIDATE_US02** - *Senha válida [ContaCerta.Domain.Users.Validates.PasswordValidate]*
- Não nula e não é espaços em branco
- Deve ter mais de 8 caracteres
- Deve ter pelo menos uma letra minúscula
- Deve ter pelo menos uma letra maiúscula
- Deve ter pelo menos um número

**VALIDATE_CO01** - *Custo válido [ContaCerta.Domain.Costs.Validates.CostValidate]*
- O titulo do custo não pode ser vazio;
- O usuário deve ser válido;
- O valor não pode ser negativo;
    