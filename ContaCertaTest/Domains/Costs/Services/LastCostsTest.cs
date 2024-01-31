using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using Moq;

namespace ContaCerta.Tests.Domains.Costs.Services
{
    public class LastCostsTest
    {
        [Fact]
        public void Execute_ValidUserWithoutLastDays_ReturnsArrayExistingLastCostOfDefaultLastDays()
        {
            var user = new User("email1@mail.com", "********", true);
            var lastCosts = new[]
            {
                new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(-5), user),
                new Cost("Conta de água", "", 50, DateTime.Now.AddDays(-2), user),
                new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(-3), user)
            };
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.LastCostsByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(lastCosts);

            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            var costs = myLastCosts.Execute(user);

            Assert.True(costs.Length == lastCosts.Length);
        }

        [Fact]
        public void Execute_ValidUserWithValidLastDays_ReturnsArrayExistingLastCost()
        {
            int lastDays = 20;
            var user = new User("email1@mail.com", "********", true);
            var lastCosts = new[]
            {
                new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(-5), user),
                new Cost("Conta de água", "", 50, DateTime.Now.AddDays(-2), user),
                new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(-3), user)
            };
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.LastCostsByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(lastCosts);

            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            var costs = myLastCosts.Execute(user, lastDays);

            Assert.True(costs.Length == lastCosts.Length);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-9)]
        [InlineData(-20)]
        [InlineData(-30)]
        public void Execute_ValidUserWithInvalidLastDays_ReturnArgumentException(int _lastDays)
        {
            var user = new User("email1@mail.com", "********", true);
            var costRepositoryMock = new Mock<ICostRepository>();
            
            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user, _lastDays);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("O número de dias deve ser maior ou igual a zero", exception.Message);
        }

        [Fact]
        public void Execute_InvalidUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();

            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(null);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InactiveUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();
            var user = new User();

            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InvalidCostRepository_ReturnsException()
        {
            int lastDays = 5;
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.LastCostsByUserId(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var user = new User("email1@mail.com", "********", true);

            var myLastCosts = new LastCosts(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user, lastDays);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro na consulta dos ultimos "+ lastDays + " custos", exception.Message);
        }
    }
}
