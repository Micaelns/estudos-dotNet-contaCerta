using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Services;
using ContaCerta.Test.Mocks;
using Moq;

namespace ContaCerta.Test.Domains.Costs.Services;
public class FindCostTest
{
    private readonly Mock<ICostRepository> _costRepositoryMock;

    public FindCostTest()
    {
        _costRepositoryMock = new Mock<ICostRepository>();
    }

    [Fact]
    public void Execute_ValidIdCost_ReturnsCostSuccessfully()
    {
        //Arrange
        int id = 1;
        Cost costMocked = CostMock.Generate();
        costMocked.Id = id;
        _costRepositoryMock.Setup(x => x.Find(It.IsAny<int>())).Returns(costMocked);
        var findCost = new FindCost(_costRepositoryMock.Object);

        //Act
        var result = findCost.Execute(id);

        //Assert
        Assert.IsType<Cost>(result);
        Assert.Equal(id, result.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-9)]
    [InlineData(-20)]
    [InlineData(-30)]
    public void Execute_InvalidIdCost_ReturnsArgumentException(int InvalidId)
    {
        //Arrange
        var findCost = new FindCost(_costRepositoryMock.Object);
        Action Act = () => findCost.Execute(InvalidId);

        //Act
        var exception = Assert.Throws<ArgumentException>(Act);

        //Assert
        Assert.Contains("Código do Custo inválido", exception.Message);
    }
}
