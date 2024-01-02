# estudosCSharp

- Projeto Conta CertaTests
    - Testar ContaCertaApi.

*  Ficha Técnica, como foi criado o projeto:  
    - Criar projeto de Testes
        > dotnet new xunit
    - Instalar o Pacote NuGet do Moq
        > dotnet add package Moq --version 4.16.1
            >>Obs: Houve conflito com nugts usados em minha máquina. Para contornar a situação, configurei um nuget para ser usado somente neste projeto: <b>dotnet new nugetconfig</b>
    - Rodar Projeto
        > dotnet test