using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using Moq;

namespace ContaCerta.Tests.Domains.Costs.Services
{
    public class LastCostsNoPayByUserTest
    {
        [Fact]
        public void Execute_ValidUser_ReturnsArrayExistingLastCostNoPay()
        {
            var user = new User("email1@mail.com", "********", true);
            user.Id = 1;
            var lastCosts = new[]
            {
                new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(-5), user),
                new Cost("Conta de água", "", 50, DateTime.Now.AddDays(-2), user),
                new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(-3), user)
            };
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.LastCostsNoPayByUserId(It.IsAny<int>())).Returns(lastCosts);

            var myLastCosts = new LastCostsNoPayByUser(costRepositoryMock.Object);
            var costs = myLastCosts.Execute(user);

            Assert.True(costs.Length == lastCosts.Length);
        }

        [Fact]
        public void Execute_InvalidUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();

            var myLastCosts = new LastCostsNoPayByUser(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(null);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido", exception.Message);
        }

        [Fact]
        public void Execute_InactiveUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();
            var user = new User();

            var myLastCosts = new LastCostsNoPayByUser(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido", exception.Message);
        }

        [Fact]
        public void Execute_InvalidCostRepository_ReturnsException()
        {
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.LastCostsNoPayByUserId(It.IsAny<int>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var user = new User("email1@mail.com", "********", true);
            user.Id = 1;

            var myLastCosts = new LastCostsNoPayByUser(costRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro na consulta dos custos antigos não pagos por usuário", exception.Message);
        }
    }
}
