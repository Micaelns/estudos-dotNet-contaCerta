using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using ContaCerta.Domain.Users.Validates.Interfaces;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services
{
    public class CreateUserTest
    {
        [Fact]
        public void Execute_ValidUser_ReturnsUserSuccessfully()
        {
            string emailSended = "valid_email";
            string passwordSended = "valid_password";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User(emailSended, passwordSended, true));
            var emailValidateMock = new Mock<IEmailValidate>();
            emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);
            var passwordValidateMock = new Mock<IPasswordValidate>();
            passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);

            var CreateUser = new CreateUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
            var user = CreateUser.Execute(emailSended, passwordSended);

            Assert.IsType<User>(user);
            Assert.Equal(emailSended, user.Email);
        }

        [Fact]
        public void Execute_UserExisting_ReturnsArgumentException()
        {
            string emailSended = "existingEmail@gmail.com.br";
            string passwordSended = "any_password";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(new User(emailSended, passwordSended, true));
            var emailValidateMock = new Mock<IEmailValidate>();
            emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);
            var passwordValidateMock = new Mock<IPasswordValidate>();
            passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);

            var CreateUser = new CreateUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
            Action Act = () => CreateUser.Execute(emailSended, passwordSended);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Email já cadastrado", exception.Message);
        }

        [Fact]
        public void Execute_InvalidEmailUser_ReturnsArgumentException()
        {
            string emailSended = "emailInvalid.com.br";
            string passwordSended = "any_password";
            var userRepositoryMock = new Mock<IUserRepository>();
            var emailValidateMock = new Mock<IEmailValidate>();
            emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => false);
            var passwordValidateMock = new Mock<IPasswordValidate>();
            passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);

            var CreateUser = new CreateUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
            Action Act = () => CreateUser.Execute(emailSended, passwordSended);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("E-mail inválido", exception.Message);
        }

        [Fact]
        public void Execute_InvalidPasswordUser_ReturnsArgumentException()
        {
            string emailSended = "email@gmail.com.br";
            string passwordSended = "";
            var userRepositoryMock = new Mock<IUserRepository>();
            var emailValidateMock = new Mock<IEmailValidate>();
            emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);
            var passwordValidateMock = new Mock<IPasswordValidate>();
            passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => false);
            passwordValidateMock.SetupGet(v => v.Messages).Returns(new List<string> { "Mensagem Erro 1", "Mensagem Erro 2" }.AsReadOnly());

            var CreateUser = new CreateUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
            Action Act = () => CreateUser.Execute(emailSended, passwordSended);

            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Senha inválida", exception.Message);
        }

        [Fact]
        public void Execute_InvalidUserRepository_ReturnsException()
        {
            string emailSended = "valid_email";
            string passwordSended = "valid_password";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Throws(new Exception("Simulando um erro no UserRepository"));
            var emailValidateMock = new Mock<IEmailValidate>();
            emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);
            var passwordValidateMock = new Mock<IPasswordValidate>();
            passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns((string text) => true);

            var createUser = new CreateUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
            Action Act = () => createUser.Execute(emailSended, passwordSended);

            var exception = Assert.Throws<Exception>(Act);
            Assert.Contains("Erro ao salvar usuário", exception.Message);
        }
    }

}
