using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using Moq;

namespace ContaCerta.Tests.Domains.Costs.Services
{
    public class NextCostsTest
    {
        [Fact]
        public void Execute_ValidUser_ReturnsArrayExistingNextCost()
        {
            var user = new User("email1@mail.com", "********", true);
            var nextCosts = new[]
            {
                new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(5), user),
                new Cost("Conta de água", "", 50, DateTime.Now.AddDays(2), user),
                new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(3), user)
            };
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.NextCostsByUserId(It.IsAny<int>())).Returns(nextCosts);

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            var costs = myNextCosts.Execute(user);

            Assert.True(costs.Length == nextCosts.Length);
        }

        [Fact]
        public void Execute_validUser_ReturnsEmpyWhenNoExistNextCost()
        {
            var user = new User("email1@mail.com", "********", true);

            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.NextCostsByUserId(It.IsAny<int>())).Returns(Array.Empty<Cost>());

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            var costs = myNextCosts.Execute(user);

            Assert.Empty(costs);
        }

        [Fact]
        public void Execute_InvalidUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(null);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InactiveUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();
            var user = new User();

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(user);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InvalidCostRepository_ReturnsException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.NextCostsByUserId(It.IsAny<int>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var user = new User("email1@mail.com", "********", true);

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(user);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro na consulta dos próximos custos", exception.Message);
        }
    }
}
