using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services
{
    public class LastCostsNoPayTest
    {
        [Fact]
        public void Execute_ValidUser_ReturnsArrayExistingLastCostNoPay()
        {
            var user = new User("email1@mail.com", "********", true);
            user.Id = 1;
            var cost = new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(-5), user);
            var cost2 = new Cost("Conta de água", "", 50, DateTime.Now.AddDays(-2), user);
            var cost3 = new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(-3), user);
            var lastCosts = new[]
            {
                new UserCost(user, cost),
                new UserCost(user, cost2),
                new UserCost(user, cost3),
            };
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            userCostRepositoryMock.Setup(x => x.LastUserCostNoPayByUser(It.IsAny<User>())).Returns(lastCosts);

            var myLastCosts = new LastCostsNoPay(userCostRepositoryMock.Object);
            var costs = myLastCosts.Execute(user);

            Assert.True(costs.Length == lastCosts.Length);
        }

        [Fact]
        public void Execute_InvalidUser_ReturnArgumentException()
        {
            var userCostRepositoryMock = new Mock<IUserCostRepository>();

            var myLastCosts = new LastCostsNoPay(userCostRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(null);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido", exception.Message);
        }

        [Fact]
        public void Execute_InactiveUser_ReturnArgumentException()
        {
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            var user = new User();

            var myLastCosts = new LastCostsNoPay(userCostRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido", exception.Message);
        }

        [Fact]
        public void Execute_InvalidUserCostRepository_ReturnsException()
        {
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            userCostRepositoryMock.Setup(x => x.LastUserCostNoPayByUser(It.IsAny<User>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var user = new User("email1@mail.com", "********", true);
            user.Id = 1;

            var myLastCosts = new LastCostsNoPay(userCostRepositoryMock.Object);
            Action Act = () => myLastCosts.Execute(user);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro na consulta dos custos antigos não pagos por usuário", exception.Message);
        }
    }
}
