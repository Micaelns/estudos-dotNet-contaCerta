# estudosCSharp

- Projeto Conta CertaTests
    - ContaCertaApi
    - ContaCertaTest

*  Ficha Técnica, como foi criado o projeto:  
    - Criar Solução (com extension vscode-solution-explorer)
        > dotnet new sln -n "ContaCertaApi"
    - Adicionar projeto ContaCertaTest a solucão (com extension vscode-solution-explorer)
        > dotnet sln "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApi.sln" add "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApiTest\ContaCertaApiTest.csproj"
    - Adicionar projeto ContaCerta a solucão (com extension vscode-solution-explorer)
        > dotnet sln "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApi.sln" add "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApi\ContaCertaApi.csproj"
    - Adicionar referencia do projeto ContaCertaApi ao projeto ContaCertaApiTests  (com extension vscode-solution-explorer)
        > dotnet add "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApiTest\ContaCertaApiTest.csproj" reference "c:\Users\4905091462\Documents\projetos\estudos\CSharp\ContaCerta\ContaCertaApi\ContaCertaApi.csproj"
    