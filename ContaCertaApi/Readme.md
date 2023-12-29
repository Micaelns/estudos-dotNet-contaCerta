# estudosCSharp

- Projeto Conta Certa
    - MultiUsuário para ser possível dividir gastos de maneira justa entre os participantes.
    - Podendo Criar enquetes para aprovação de gastos.

*  Ficha Técnica, como foi criado o projeto:  
    - Criar projeto WebApi
        > dotnet new webapi
    - Rodar Projeto
        > dotnet watch run
    - Instalar ferramenta do entityFramework. Isso permite rodar comandos do entityFramework diretamente no console no dotnet. Considerando que essa aplicação estou rodando no vsCode;
        - dotnet tool install --global dotnet-ef
        - Obs1.: Houve conflito com nugts usados em minha máquina. Para contornar a situação, configurei um nuget para ser usado somente neste projeto:
        > dotnet new nugetconfig
        - Obs2.: Como meu dotnet não era compatível com o dotnet-ef mais atual tive que fazer um downGrade com o comando abaixo:
        > dotnet tool install --global dotnet-ef --version 7.0.0
    - dotnet add package Microsoft.EntityFrameworkCore.Design
        - Obs: Como meu dotnet não era compatível com o dotnet-ef mais atual tive que fazer um downGrade com o comando abaixo:
        > dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0
    - dotnet add package Microsoft.EntityFrameworkCore.SqlServer
        - Obs: Como meu dotnet não era compatível com o dotnet-ef mais atual tive que fazer um downGrade com o comando abaixo:
        > dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.0
    - buildar projeto
        > dotnet build
    - limpar cache
        > dotnet clean

- Migrations:
    - Criar migration baseado em mudanças no Context criado, no caso (AgendaContext)
        > dotnet-ef migrations add CriacaoTabelaContato
    - Rodar migrations pendentes:
        > dotnet-ef database update