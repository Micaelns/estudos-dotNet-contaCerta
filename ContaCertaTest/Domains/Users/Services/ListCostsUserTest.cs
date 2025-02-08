using ContaCerta.Domain.Costs;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services;

public class ListCostsUserTest
{
    readonly int DefaultLastDays = 15;

    [Fact]
    public void ListCostsLastDays_ValidUserWithoutLastDays_ReturnsLastCostOfDefaultLastDays()
    {
        var user = new User() { Active = true };
        int lastDaysUsed = 0;
        var lastCosts = new[]
        {
            new UserCost() { User = user, Cost = new Cost() { Id = 10 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 11 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 12 } },
        };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.LastUserCostsByUser(It.IsAny<User>(), It.IsAny<int>()))
                                .Callback<User, int>( (user, lastDays) => lastDaysUsed = lastDays)
                                .Returns(lastCosts);

        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        var costs = myLastCosts.ListCostsLastDays(user);

        Assert.True(costs.Length == lastCosts.Length);
        Assert.Equal(DefaultLastDays, lastDaysUsed);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-9)]
    [InlineData(-20)]
    [InlineData(-30)]
    public void ListCostsLastDays_ValidUserWithNegativeLastDays_ReturnsSuccessWithPositiveLastDays(int lastDaysParams)
    {
        var user = new User() { Active = true };
        int lastDaysUsed = 0;
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.LastUserCostsByUser(It.IsAny<User>(), It.IsAny<int>()))
                                .Callback<User, int>((user, lastDays) => lastDaysUsed = lastDays)
                                .Returns([]);

        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        var costs = myLastCosts.ListCostsLastDays(user, lastDaysParams);

        Assert.Equal(Math.Abs(lastDaysParams), lastDaysUsed);
    }

    [Fact]
    public void ListCostsLastDays_InvalidUser_ReturnArgumentException()
    {
        var user = new User() { Active = false };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();

        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        Action Act = () => myLastCosts.ListCostsLastDays(user);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void ListCostsLastDays_InvalidUserCostRepository_ReturnsException()
    {
        var user = new User() { Active = true };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.LastUserCostsByUser(It.IsAny<User>(), It.IsAny<int>())).Throws(new Exception("Simulando um erro no UserCostRepository"));
        
        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        Action Act = () => myLastCosts.ListCostsLastDays(user);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains("Erro na consulta de custos", exception.Message);
    }

    [Fact]
    public void NextCosts_ValidUser_ReturnsSuccess()
    {
        var user = new User() { Active = true };
        var nextCosts = new[]
        {
            new UserCost() { User = user, Cost = new Cost() { Id = 10 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 11 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 12 } },
        };
        var costRepositoryMock = new Mock<IUserCostRepository>();
        costRepositoryMock.Setup(x => x.NextUserCostByUser(It.IsAny<User>())).Returns(nextCosts);

        var myNextCosts = new ListCostsUser(costRepositoryMock.Object);
        var costs = myNextCosts.NextCosts(user);

        Assert.True(costs.Length == nextCosts.Length);
    }

    [Fact]
    public void NextCosts_InvalidUser_ReturnsArgumentException()
    {
        var user = new User() { Active = false };
        var costRepositoryMock = new Mock<IUserCostRepository>();
         
        var myNextCosts = new ListCostsUser(costRepositoryMock.Object);
        Action Act = () => myNextCosts.NextCosts(user);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void NextCosts_InvalidUserCostRepository_ReturnsException()
    {
        var user = new User() { Active = true };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.NextUserCostByUser(It.IsAny<User>())).Throws(new Exception("Simulando um erro no UserCostRepository"));

        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        Action Act = () => myLastCosts.NextCosts(user);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains(MessageCost.ErrorNextDaysQuery, exception.Message);
    }

    [Fact]
    public void LastCostsNoPay_ValidUser_ReturnsSuccess()
    {
        var user = new User() { Active = true };
        var nextCosts = new[]
        {
            new UserCost() { User = user, Cost = new Cost() { Id = 10 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 11 } },
            new UserCost() { User = user, Cost = new Cost() { Id = 12 } },
        };
        var costRepositoryMock = new Mock<IUserCostRepository>();
        costRepositoryMock.Setup(x => x.LastUserCostNoPayByUser(It.IsAny<User>())).Returns(nextCosts);

        var myNextCosts = new ListCostsUser(costRepositoryMock.Object);
        var costs = myNextCosts.LastCostsNoPay(user);

        Assert.True(costs.Length == nextCosts.Length);
    }

    [Fact]
    public void LastCostsNoPay_InvalidUser_ReturnsArgumentException()
    {
        var user = new User() { Active = false };
        var costRepositoryMock = new Mock<IUserCostRepository>();

        var myNextCosts = new ListCostsUser(costRepositoryMock.Object);
        Action Act = () => myNextCosts.LastCostsNoPay(user);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void LastCostsNoPay_InvalidUserCostRepository_ReturnsException()
    {
        var user = new User() { Active = true };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.LastUserCostNoPayByUser(It.IsAny<User>())).Throws(new Exception("Simulando um erro no UserCostRepository"));

        var myLastCosts = new ListCostsUser(userCostRepositoryMock.Object);
        Action Act = () => myLastCosts.LastCostsNoPay(user);

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains(MessageCost.ErrorCostQueryNoPay, exception.Message);
    }
}
