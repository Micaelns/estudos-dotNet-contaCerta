using ContaCerta.Domain.Costs;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using Moq;

namespace ContaCerta.Tests.Domain.Costs.Services;

public class ManagerUserInCostTest
{
    [Fact]
    public void RemoveUsers_withoutUsersArray_OnlyReturn()
    {
        var validCost = new Cost() { Active = true };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.RemoveUsers([], validCost);

        userCostRepositoryMock.Verify(x => x.ListUserCostsByCost(It.IsAny<Cost>()), Times.Never);
    }

    [Fact]
    public void RemoveUsers_withInactiveCost_ReturnArgumentException()
    {
        var invalidCost = new Cost() { Active = false };
        User[] usersToRemove = [new()];
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        Action Act = () => managerUser.RemoveUsers(usersToRemove, invalidCost);
        var exception = Assert.Throws<ArgumentException>(Act);

        Assert.Contains(MessageCost.InvalidCost, exception.Message);
    }

    [Fact]
    public void RemoveUsers_withoutUsersSaved_OnlyReturn()
    {
        var validCost = new Cost() { Active = true };
        User[] usersToRemove = [new()];

        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(Array.Empty<UserCost>());
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.RemoveUsers(usersToRemove, validCost);

        userCostRepositoryMock.Verify(x => x.ListUserCostsByCost(It.IsAny<Cost>()), Times.Once);
        userCostRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Never);
    }

    [Fact]
    public void RemoveUsers_withUserCostsPaid_ReturnException()
    {
        var validCost = new Cost() { Active = true };
        User[] usersToRemove = [new()];
        var userCostPaid = new UserCost() { User = new User(), Payed = true };
        var listUserCost = new UserCost[] { new(), userCostPaid };
        
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        Action Act = () => managerUser.RemoveUsers(usersToRemove, validCost);
        var exception = Assert.Throws<Exception>(Act);

        Assert.Contains(MessageCost.ImpossibleManagerUsersIfAnyPaid, exception.Message);
    }

    [Fact]
    public void RemoveUsers_withUsersNoSaved_ReturnWithoutEdit()
    {
        var validCost = new Cost() { Active = true };
        User[] usersToRemove = [new() { Id=55 }];
        var userCost1 = new UserCost() { User = new() { Id = 10 } };
        var userCost2 = new UserCost() { User = new() { Id = 20 } };
        var listUserCost = new UserCost[] { userCost1, userCost2 };

        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.RemoveUsers(usersToRemove, validCost);

        userCostRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Never);
    }

    [Fact]
    public void RemoveUsers_withValidsParams_EditedWithSuccess()
    {
        var validCost = new Cost() { Active = true, Value = 90 };
        User[] usersToRemove = [new() { Id = 30 }];
        var userCost1 = new UserCost() { Id= 10, User = new() { Id = 10 }, Value = 30 };
        var userCost2 = new UserCost() { Id = 20, User = new() { Id = 20 }, Value = 30 };
        var userCost3 = new UserCost() { Id = 30, User = new() { Id = 30 }, Value = 30 };
        var expectNewValueUserCost = (float)Math.Round((validCost.Value / 2), 2);
        var sendToSave = new List<bool>();
        var listUserCost = new UserCost[] { userCost1, userCost2, userCost3 };

        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
        userCostRepositoryMock.Setup(x => x.Save(It.IsAny<UserCost>()))
                                .Callback<UserCost>(userCost => sendToSave.Add(expectNewValueUserCost == userCost.Value));

        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.RemoveUsers(usersToRemove, validCost);

        userCostRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Exactly(2));
        Assert.True(sendToSave.All( item => item));
    }

    [Fact]
    public void AddUsers_withoutUsersArray_OnlyReturn()
    {
        var validCost = new Cost() { Active = true };
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.AddUsers([], validCost);

        userCostRepositoryMock.Verify(x => x.ListUserCostsByCost(It.IsAny<Cost>()), Times.Never);

    }

    [Fact]
    public void AddUsers_withInactiveCost_ReturnArgumentException()
    {
        var invalidCost = new Cost() { Active = false };
        User[] usersToAdd = [new()];
        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        Action Act = () => managerUser.AddUsers(usersToAdd, invalidCost);
        var exception = Assert.Throws<ArgumentException>(Act);

        Assert.Contains(MessageCost.InvalidCost, exception.Message);
    }

    [Fact]
    public void AddUsers_withUserCostsPaid_ReturnException()
    {
        var validCost = new Cost() { Active = true };
        User[] usersToAdd = [new()];
        var userCostPaid = new UserCost() { User = new User(), Payed = true };
        var listUserCost = new UserCost[] { new(), userCostPaid };

        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        Action Act = () => managerUser.AddUsers(usersToAdd, validCost);
        var exception = Assert.Throws<Exception>(Act);

        Assert.Contains(MessageCost.ImpossibleManagerUsersIfAnyPaid, exception.Message);
    }

    [Fact]
    public void AddUsers_withUsersAlreadySaved_ReturnWithoutAdd()
    {
        var validCost = new Cost() { Active = true };
        User[] usersToAdd = [new() { Id = 10 } ];
        var userCost1 = new UserCost() { User = new() { Id = 10 } };
        var userCost2 = new UserCost() { User = new() { Id = 20 } };
        var listUserCost = new UserCost[] { userCost1, userCost2 };

        var userCostRepositoryMock = new Mock<IUserCostRepository>();
        userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
        var managerUser = new ManagerUsersInCost(userCostRepositoryMock.Object);

        managerUser.AddUsers(usersToAdd, validCost);

        userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Never);
    }
}
