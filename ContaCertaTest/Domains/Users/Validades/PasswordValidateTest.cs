
namespace ContaCerta.Domain.Users.Validates
{
    public class PasswordValidateTest
    {
        [Fact]
        public void Execute_ValidPassword_ReturnTrue()
        {
            string passwordSended = "abc123ABC";
            var passwordValidate = new PasswordValidate();
            
            var isValid = passwordValidate.IsValid(passwordSended);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("",1)]
        [InlineData("  ",1)]
        [InlineData("asd",3)]
        [InlineData("asdasdasd",2)]
        [InlineData("asdasdasd12345",1)]
        [InlineData("asdABCdefgh",1)]
        [InlineData("ABCDEFGHI",2)]
        [InlineData("ABC123456789",1)]
        public void Execute_InvalidPassword_ReturnFalseAndQtdErrorsCorrect(string invalidPassword, int qtdErrors)
        {
            var passwordValidate = new PasswordValidate();
            
            var isValid = passwordValidate.IsValid(invalidPassword);

            Assert.False(isValid);
            Assert.Equal(qtdErrors, passwordValidate.Messages.Count);
        }
    }
}