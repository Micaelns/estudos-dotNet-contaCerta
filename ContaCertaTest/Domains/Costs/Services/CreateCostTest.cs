using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Model;
using Moq;

namespace ContaCerta.Tests.Domain.Costs.Services
{
    public class CreateCostTest
    {
        [Fact]
        public void Execute_ValidDataToCreateCost_ReturnsCostSuccessfully()
        {
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.Save(It.IsAny<Cost>())).Returns(new Cost(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended));
            var costValidateMock = new Mock<ICostValidate>();
            costValidateMock.Setup(x => x.IsValid(It.IsAny<Cost>())).Returns(true);

            var createCost = new CreateCost(costRepositoryMock.Object, costValidateMock.Object);
            var cost = createCost.Execute(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

            Assert.IsType<Cost>(cost);
            Assert.Equal(titleSended, cost.Title);
            Assert.Equal(descriptionSended, cost.Description);
            Assert.Equal(userRequestedSended, cost.UserRequested);
        }

        [Fact]
        public void Execute_InvalidDataToCreateCost_ReturnsArgumentException()
        {
            string titleSended = "invalid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.Save(It.IsAny<Cost>())).Returns(new Cost(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended));
            var costValidateMock = new Mock<ICostValidate>();
            costValidateMock.Setup(x => x.IsValid(It.IsAny<Cost>())).Returns(false);
            costValidateMock.SetupGet(v => v.Messages).Returns(new List<string> { "Mensagem Erro 1", "Mensagem Erro 2" }.AsReadOnly());

            var createCost = new CreateCost(costRepositoryMock.Object, costValidateMock.Object);
            Action Act = () => createCost.Execute(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Custo inválido", exception.Message);
        }

        [Fact]
        public void Execute_InvalidCostRepository_ReturnsException()
        {
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.Save(It.IsAny<Cost>())).Throws(new Exception("Simulando um erro no CostRepository"));
            var costValidateMock = new Mock<ICostValidate>();
            costValidateMock.Setup(x => x.IsValid(It.IsAny<Cost>())).Returns(true);

            var createCost = new CreateCost(costRepositoryMock.Object, costValidateMock.Object);
            Action Act = () => createCost.Execute(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro ao salvar custo", exception.Message);
        }
    }
}
