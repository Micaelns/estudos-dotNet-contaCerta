using ContaCerta.Domain.Costs;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users;
using ContaCerta.Domain.Users.Model;
using Moq;
using System;

namespace ContaCerta.Tests.Domain.Costs.Services;

public class ManagerCostTest
{
    [Fact]
    public void Create_ValidDataToCreateCost_ReturnsCostSuccessfully()
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

        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        var cost = createCost.Create(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

        Assert.IsType<Cost>(cost);
        Assert.Equal(titleSended, cost.Title);
        Assert.Equal(descriptionSended, cost.Description);
        Assert.Equal(userRequestedSended, cost.UserRequested);
    }

    [Fact]
    public void Create_InvalidDataToCreateCost_ReturnsArgumentException()
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
        costValidateMock.SetupGet(v => v.ErrorMessages).Returns("Mensagens Erro");

        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => createCost.Create(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void Create_InvalidCostRepository_ReturnsException()
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

        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => createCost.Create(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

        var exception = Assert.Throws<Exception>(Act);
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void Find_InvalidId_ReturnsArgumentException()
    {
        int zeroOrNegativeValue = 0;
        var costRepositoryMock = new Mock<ICostRepository>();
        var costValidateMock = new Mock<ICostValidate>();
        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);

        Action Act = () => createCost.Find(zeroOrNegativeValue);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageCost.InvalidCost, exception.Message);
    }

    [Fact]
    public void Find_IdNoExists_ReturnsException()
    {
        int id = 20;
        var costRepositoryMock = new Mock<ICostRepository>();
        var costValidateMock = new Mock<ICostValidate>();
        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);

        Action Act = () => createCost.Find(id);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains(MessageCost.InvalidCost, exception.Message);
    }

    [Fact]
    public void Find_ValidId_ReturnsValidCost()
    {
        int id = 20;
        var expectedCost = new Cost() { Id = id};
        var costRepositoryMock = new Mock<ICostRepository>();
        var costValidateMock = new Mock<ICostValidate>();
        costRepositoryMock.Setup(x => x.Find(It.IsAny<int>())).Returns(expectedCost);

        var createCost = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        var result = createCost.Find(id);

        Assert.IsType<Cost>(result);
        Assert.Equal(id, result.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-9)]
    [InlineData(-20)]
    [InlineData(-30)]
    public void LastCostsCreatedByUser_ValidUserWithInvalidLastDays_ReturnArgumentException(int _lastDays)
    {
        var user = new User() { Active = true};
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();

        var myLastCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => myLastCosts.LastCostsCreatedByUser(user, _lastDays);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageCost.InvalidNumberOfDays, exception.Message);
    }

    [Fact]
    public void LastCostsCreatedByUser_InvalidUser_ReturnArgumentException()
    {
        var invalidUser = new User() { Active = false };
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();

        var myLastCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => myLastCosts.LastCostsCreatedByUser(invalidUser);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void LastCostsCreatedByUser_ValidUserWithoutLastDays_UseDefaultLastDays()
    {
        var user = new User() { Active = true };
        int expectedDefaultLastDays = 15;
        int defaultLastDays = 0;
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();
        costRepositoryMock.Setup(x => x.LastCostsCreatedByUserId(It.IsAny<int>(), It.IsAny<int>()))
                               .Callback<int, int>( (int userId, int lastDays) => defaultLastDays = lastDays);


        var myLastCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        var result = myLastCosts.LastCostsCreatedByUser(user);

        costRepositoryMock.Verify(x => x.LastCostsCreatedByUserId(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        Assert.Equal(expectedDefaultLastDays, defaultLastDays);
    }

    [Fact]
    public void LastCostsCreatedByUser_ValidUser_ReturnsCostsSuccessfully()
    {
        var validUser = new User() { Active = true };
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();
        var lastCosts = new[] { new Cost(), new Cost() };

        costRepositoryMock.Setup(x => x.LastCostsCreatedByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(lastCosts);

        var myLastCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        var result = myLastCosts.LastCostsCreatedByUser(validUser);

        Assert.NotEmpty(result);
        Assert.IsType<Cost[]>(result);
    }

    [Fact]
    public void LastCostsCreatedByUser_InvalidCostRepository_ReturnsException()
    {
        var validUser = new User() { Active = true };
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();
        costRepositoryMock.Setup(x => x.LastCostsCreatedByUserId(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Simulando um erro no CostRepository"));
        
        var myLastCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => myLastCosts.LastCostsCreatedByUser(validUser);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains("Erro na consulta", exception.Message);
    }

    [Fact]
    public void NextCostsCreatedByUser_InvalidUser_ReturnArgumentException()
    {
        var invalidUser = new User() { Active = false };
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();

        var myNextCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => myNextCosts.NextCostsCreatedByUser(invalidUser);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void NextCostsCreatedByUser_ValidUser_ReturnsArrayExistingNextCostCreated()
    {
        var validUser = new User() { Active = true };
        var nextCosts = new[] { new Cost(),new Cost(), new Cost()};
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();
        costRepositoryMock.Setup(x => x.NextCostsCreatedByUserId(It.IsAny<int>())).Returns(nextCosts);

        var myNextCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        var result = myNextCosts.NextCostsCreatedByUser(validUser);

        Assert.NotEmpty(result);
        Assert.IsType<Cost[]>(result);
    }

    [Fact]
    public void NextCostsCreatedByUser_InvalidCostRepository_ReturnsException()
    {
        var validUser = new User() { Active = true };
        var costValidateMock = new Mock<ICostValidate>();
        var costRepositoryMock = new Mock<ICostRepository>();
        costRepositoryMock.Setup(x => x.NextCostsCreatedByUserId(It.IsAny<int>())).Throws(new Exception("Simulando um erro no CostRepository"));

        var myNextCosts = new ManagerCost(costRepositoryMock.Object, costValidateMock.Object);
        Action Act = () => myNextCosts.NextCostsCreatedByUser(validUser);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains("Erro na consulta", exception.Message);
    }
}
