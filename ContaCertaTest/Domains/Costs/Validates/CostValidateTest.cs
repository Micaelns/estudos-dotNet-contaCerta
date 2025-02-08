using ContaCerta.Domain.Costs;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Validates;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Tests.Domain.Costs.Validates;

public class CostValidateTest
{
    [Fact]
    public void IsValid_ValidDataToCreateCost_ReturnTrue()
    {
        string titleSended = "valid_title";
        float valueSended = 1;
        User userRequestedSended = new() { Active = true };
        var costValid = new Cost()
        {
            Title = titleSended,
            Value = valueSended,
            UserRequested = userRequestedSended
        };
        
        var costValidate = new CostValidate();
        var isValid = costValidate.IsValid(costValid);

        Assert.True(isValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    public void IsValid_InvalidTitleOnDataToCreateCost_ReturnFalse(string invalidTitle)
    {
        float valueSended = 1;
        User userRequestedSended = new() { Active = true };
        var costValid = new Cost()
        {
            Title = invalidTitle,
            Value = valueSended,
            UserRequested = userRequestedSended
        };
        var costValidate = new CostValidate();
        var isValid = costValidate.IsValid(costValid);

        Assert.False(isValid);
        Assert.Contains(MessageCost.TitleCanNotEmpty, costValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidUserOnDataToCreateCost_ReturnFalse()
    {
        string titleSended = "valid_title";
        float valueSended = 1;
        User userRequestedSended = new() { Active = false };
        var costValid = new Cost()
        {
            Title = titleSended,
            Value = valueSended,
            UserRequested = userRequestedSended
        };
        var costValidate = new CostValidate();
        var isValid = costValidate.IsValid(costValid);

        Assert.False(isValid);
        Assert.Contains(MessageCost.UserCanNotInvalid, costValidate.ErrorMessages);
    }

    [Theory]
    [InlineData(-1.4)]
    [InlineData(-45.0)]
    [InlineData(-59.3)]
    [InlineData(-0.003)]
    [InlineData(-2.0)]
    [InlineData(-1399.3)]
    public void IsValid_InvalidValueOnDataToCreateCost_ReturnFalse(float invalidValue)
    {
        string titleSended = "valid_title";
        User userRequestedSended = new() { Active = true };
        var costValid = new Cost()
        {
            Title = titleSended,
            Value = invalidValue,
            UserRequested = userRequestedSended
        };

        var costValidate = new CostValidate();
        var isValid = costValidate.IsValid(costValid);

        Assert.False(isValid);
        Assert.Contains(MessageCost.ValueCanNotNegative, costValidate.ErrorMessages);
    }
}
