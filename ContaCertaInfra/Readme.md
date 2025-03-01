# estudosCSharp


*  Ficha Técnica, como foi criado o projeto:  
    - Instalar ferramenta do entityFramework. Isso permite rodar comandos do entityFramework diretamente no console no dotnet.
    - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    - buildar projeto Principal
        > dotnet build
    - limpar cache
        > dotnet clean

- Migrations:
    - Criar migration baseado em mudanças no Context criado (ContaCertaContext);
        - Obs.: Como toda injeção de dependência está no Projeto Api, devemos rodar com base nela;
        > ``dotnet ef migrations add NomeMigration --project ContaCerta.Infra.csproj --startup-project ../ContaCertaApi/ContaCerta.Api.csproj``
        
        > ``dotnet ef migrations add NomeMigration``
    - Para desfazer Rodar:
        > ``dotnet ef migrations remove --project ContaCerta.Infra.csproj --startup-project ../ContaCertaApi/ContaCerta.Api.csproj``
        
        > ``dotnet ef migrations remove``
    - Rodar migrations pendentes:
        > ``dotnet-ef database update --project ContaCerta.Infra.csproj --startup-project ../ContaCertaApi/ContaCerta.Api.csproj``
        
        > ``dotnet-ef database update``