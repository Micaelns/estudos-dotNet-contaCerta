
namespace ContaCertaApi.src.Domains.Users.Validates
{
    public class PasswordValidateTest
    {
        [Fact]
        public void PasswordValidate_ValidPassword_ReturnTrue()
        {
            string passwordSended = "abc123ABC";
            var passwordValidate = new PasswordValidate();
            
            var isValid = passwordValidate.Execute(passwordSended);

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
        public void PasswordValidate_InvalidPassword_ReturnFalseAndQtdErrorsCorrect(string invalidPassword, int qtdErrors)
        {
            var passwordValidate = new PasswordValidate();
            
            var isValid = passwordValidate.Execute(invalidPassword);

            Assert.False(isValid);
            Assert.Equal(qtdErrors, passwordValidate.Messages.Count);
        }
    }
}