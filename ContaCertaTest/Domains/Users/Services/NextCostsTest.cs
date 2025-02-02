using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services
{
    public class NextCostsTest
    {
        [Fact]
        public void Execute_ValidUser_ReturnsArrayExistingNextCosts()
        {
            var user = new User("email1@mail.com", "********", true);
            var cost = new Cost("Conta de luz", "", 100, DateTime.Now.AddDays(5), user);
            var cost2 = new Cost("Conta de água", "", 50, DateTime.Now.AddDays(2), user);
            var cost3 = new Cost("Conta de telefone", "", 150, DateTime.Now.AddDays(3), user);
            var nextCosts = new[]
            {
                new UserCost(user, cost),
                new UserCost(user, cost2),
                new UserCost(user, cost3),
            };
            var costRepositoryMock = new Mock<IUserCostRepository>();
            costRepositoryMock.Setup(x => x.NextUserCostByUser(It.IsAny<User>())).Returns(nextCosts);

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            var costs = myNextCosts.Execute(user);

            Assert.True(costs.Length == nextCosts.Length);
        }

        [Fact]
        public void Execute_validUser_ReturnsEmpyWhenNoExistNextCost()
        {
            var user = new User("email1@mail.com", "********", true);

            var costRepositoryMock = new Mock<IUserCostRepository>();
            costRepositoryMock.Setup(x => x.NextUserCostByUser(It.IsAny<User>())).Returns(Array.Empty<UserCost>());

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            var costs = myNextCosts.Execute(user);

            Assert.Empty(costs);
        }

        [Fact]
        public void Execute_InvalidUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<IUserCostRepository>();

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(null);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InactiveUser_ReturnArgumentException()
        {
            var costRepositoryMock = new Mock<IUserCostRepository>();
            var user = new User();

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(user);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Usuário inválido ou inativo", exception.Message);
        }

        [Fact]
        public void Execute_InvalidCostRepository_ReturnsException()
        {
            var costRepositoryMock = new Mock<IUserCostRepository>();
            costRepositoryMock.Setup(x => x.NextUserCostByUser(It.IsAny<User>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var user = new User("email1@mail.com", "********", true);

            var myNextCosts = new NextCosts(costRepositoryMock.Object);
            Action Act = () => myNextCosts.Execute(user);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro na consulta dos próximos custos", exception.Message);
        }
    }
}
