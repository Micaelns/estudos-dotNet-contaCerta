using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;
using Moq;

namespace ContaCerta.Domain.Costs.Services.Tests
{
    public class CreateCostTest
    {
        [Fact]
        public void Execute_ValidCost_ReturnsCostSuccessfully()
        {
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costRepositoryMock = new Mock<ICostRepository>();
            costRepositoryMock.Setup(x => x.Save(It.IsAny<Cost>())).Returns(new Cost(titleSended, descriptionSended, valueSended, userRequestedSended, activeSended));

            var createCost = new CreateCost(costRepositoryMock.Object);
            var cost = createCost.Execute(titleSended, descriptionSended, valueSended, userRequestedSended, activeSended);

            Assert.IsType<Cost>(cost);
            Assert.Equal(titleSended, cost.Title);
            Assert.Equal(descriptionSended, cost.Description);
            Assert.Equal(userRequestedSended, cost.UserRequested);
        }
    }
}
