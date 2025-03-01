using ContaCerta.Domain.Users;
using ContaCerta.Domain.Users.Validates;

namespace ContaCerta.Tests.Domain.Users.Validates;

public class PasswordValidateTest
{
    [Fact]
    public void IsValid_ValidPassword_ReturnTrue()
    {
        string passwordSended = "abc123ABC";
        var passwordValidate = new PasswordValidate();
        
        var isValid = passwordValidate.IsValid(passwordSended);

        Assert.True(isValid);
        Assert.Empty(passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordSort_ReturnFalse()
    {
        var invalidPassword = "AA22a";
        var passwordValidate = new PasswordValidate();
        var preparedMessage = MessageUser.InvalidPasswordSort.Replace("{0}", passwordValidate.MinLength.ToString());

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(preparedMessage, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoNumberNoLowerCase_ReturnFalse()
    {
        var invalidPassword = "AAAAAAAAA";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordNoNumbers, passwordValidate.ErrorMessages);
        Assert.Contains(MessageUser.InvalidPasswordNoLowerCase, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoUpperCaseNoLowerCase_ReturnFalse()
    {
        var invalidPassword = "123456789";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordNoUpperCase, passwordValidate.ErrorMessages);
        Assert.Contains(MessageUser.InvalidPasswordNoLowerCase, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoEmpty_ReturnFalse()
    {
        var invalidPassword = "  ";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordEmpty, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoUpperCase_ReturnFalse()
    {
        var invalidPassword = "asdasdasd12345";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordNoUpperCase, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoLowerCase_ReturnFalse()
    {
        var invalidPassword = "ABC123456789";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordNoLowerCase, passwordValidate.ErrorMessages);
    }

    [Fact]
    public void IsValid_InvalidPasswordNoNumbers_ReturnFalse()
    {
        var invalidPassword = "asdABCdefgh";
        var passwordValidate = new PasswordValidate();

        var isValid = passwordValidate.IsValid(invalidPassword);

        Assert.False(isValid);
        Assert.Contains(MessageUser.InvalidPasswordNoNumbers, passwordValidate.ErrorMessages);
    }
}