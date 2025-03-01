using ContaCerta.Domain.Users.Validates;

namespace ContaCerta.Tests.Domain.Users.Validates;

public class EmailValidateTest
{
    [Fact]
    public void IsValid_ValidEmail_ReturnTrue()
    {
        string validEmail = "email@dominio.com.br";
        var emailValidate = new EmailValidate();

        var isValid = emailValidate.IsValid(validEmail);

        Assert.True(isValid);
        Assert.Empty(emailValidate.ErrorMessages);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("aaa.com.br")]
    [InlineData("aaaa@aa")]
    [InlineData("aaaa@aa@aaa.aa")]
    [InlineData("-ç@a.sss")]
    [InlineData("aa@sss_s.com.br")]
    public void IsValid_InvalidEmail_ReturnFalseAndCorrectErrorMessage(string invalidEmail)
    {
        var emailValidate = new EmailValidate();

        var isValid = emailValidate.IsValid(invalidEmail);

        Assert.False(isValid);
        Assert.NotEmpty(emailValidate.ErrorMessages);
    }
}
