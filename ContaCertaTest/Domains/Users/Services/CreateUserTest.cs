using ContaCerta.Domains.Users.Validates;
using ContaCerta.Domains.Users.Model;
using ContaCerta.Domains.Users.Repositories;
using Moq;

namespace ContaCerta.Domains.Users.Services;

public class CreateUserTest
{
    [Fact]
    public void Execute_ValidUser_ReturnsUserSuccessfully()
    {
        string emailSended = "email@gmail.com.br";
        string passwordSended = "any_password";
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User(emailSended, passwordSended, true));

        var passwordValidateMock = new Mock<PasswordValidate>();
        passwordValidateMock.Setup(x => x.Execute(It.IsAny<string>())).Returns((string password) => true );

        var CreateUser = new CreateUser(userRepositoryMock.Object, new EmailValidate(), passwordValidateMock.Object);
        var user = CreateUser.Execute(emailSended, passwordSended);

        Assert.IsType<User>(user);
        Assert.Equal(emailSended, user.Email);
    }

    [Fact]
    public void Execute_UserExisting_ReturnsArgumentException()
    {
        string emailSended = "email@gmail.com.br";
        string passwordSended = "any_password";
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(new User(emailSended, passwordSended, true));
        
        var passwordValidateMock = new Mock<PasswordValidate>();
        passwordValidateMock.Setup(x => x.Execute(It.IsAny<string>())).Returns((string password) => true );

        var CreateUser = new CreateUser(userRepositoryMock.Object, new EmailValidate(), passwordValidateMock.Object);
        Action Act = () => CreateUser.Execute(emailSended, passwordSended);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains("Email já cadastrado", exception.Message);
    }

    [Fact]
    public void Execute_InvalidEmailUser_ReturnsArgumentException()
    {
        string emailSended = "emailInvalid.com.br";
        string passwordSended = "";
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User(emailSended, passwordSended, true));
        var passwordValidate = new PasswordValidate();
        var CreateUser = new CreateUser(userRepositoryMock.Object, new EmailValidate(), passwordValidate);

        Action Act = () => CreateUser.Execute(emailSended, passwordSended);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains("E-mail inválido", exception.Message);
    }

    [Fact]
    public void Execute_InvalidPasswordUser_ReturnsArgumentException()
    {
        string emailSended = "email@gmail.com.br";
        string passwordSended = "";
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User(emailSended, passwordSended, true));
        var passwordValidate = new PasswordValidate();
        var CreateUser = new CreateUser(userRepositoryMock.Object, new EmailValidate(), passwordValidate);

        Action Act = () => CreateUser.Execute(emailSended, passwordSended);

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains("Senha inválida", exception.Message);
    }

    [Fact]
    public void Execute_InvalidUserRepository_ReturnsException()
    {
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Throws(new Exception("Simulando um erro no UserRepository"));

        var passwordValidateMock = new Mock<PasswordValidate>();
        passwordValidateMock.Setup(x => x.Execute(It.IsAny<string>())).Returns(true);

        var createUser = new CreateUser(userRepositoryMock.Object, new EmailValidate(), passwordValidateMock.Object);

        Action Act = () => createUser.Execute("some@email.com", "some_password");

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains("Erro ao salvar usuário", exception.Message);
    }
}