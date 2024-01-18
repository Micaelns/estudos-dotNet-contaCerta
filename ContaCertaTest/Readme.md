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
    - Instalar ferramenta de cobertura de test
        > dotnet tool install -g coverlet.console
    - Instalar o coverlet.msbuild em todos projetos
        > dotnet add package coverlet.msbuild
    - Executar Testes com Cobertura
        > coverlet "ContaCerta.Test.dll" --target "dotnet" --targetargs "test" --format opencover --output "./TestResults/"
        > dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
    - Visualizar a Cobertura
        > dotnet tool install -g dotnet-reportgenerator-globaltool
        > reportgenerator "-reports:**/coverage.opencover.xml" "-targetdir:CoverletReports" -reporttypes:Html


