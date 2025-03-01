using ContaCerta.Domain.Users;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using ContaCerta.Domain.Users.Validates.Interfaces;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services;

public class ManagerUserTest
{
    [Fact]
    public void FindActiveByEmail_ValidUser_ReturnsUserSuccessfully()
    {
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(new User() { Active = true});

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        var user = CreateUser.FindActiveByEmail(It.IsAny<string>());

        Assert.IsType<User>(user);
        Assert.NotNull(user);
    }

    [Fact]
    public void FindActiveByEmail_InvalidUser_ReturnsArgumentException()
    {
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
        emailValidateMock.SetupGet(v => v.ErrorMessages).Returns(It.IsAny<string>());
        var passwordValidateMock = new Mock<IPasswordValidate>();
        var userRepositoryMock = new Mock<IUserRepository>();
        
        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.FindActiveByEmail(It.IsAny<string>());

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void FindActiveByEmail_NoExistsUser_ReturnsArgumentException()
    {
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.FindActiveByEmail(It.IsAny<string>());

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.InvalidUser, exception.Message);
    }

    [Fact]
    public void FindActiveByEmail_InvalidUserRepository_ReturnsException()
    {
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>())).Throws(new Exception("Simulando um erro no UserRepository"));

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.FindActiveByEmail(It.IsAny<string>());

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains(MessageUser.ErrorFind, exception.Message);
    }

    [Fact]
    public void Create_ValidUser_ReturnsUserSuccessfully()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User());
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        var user = CreateUser.Create(It.IsAny<string>(), It.IsAny<string>());

        Assert.IsType<User>(user);
        Assert.NotNull(user);
    }

    [Fact]
    public void Create_UserExisting_ReturnsArgumentException()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(new User());
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.Create(It.IsAny<string>(), It.IsAny<string>());

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.Contains(MessageUser.UserExists, exception.Message);
    }

    [Fact]
    public void Create_InvalidEmailUser_ReturnsArgumentException()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
        emailValidateMock.SetupGet(v => v.ErrorMessages).Returns(It.IsAny<string>());
        var passwordValidateMock = new Mock<IPasswordValidate>();
        passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.Create(It.IsAny<string>(), It.IsAny<string>());

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void Create_InvalidPasswordUser_ReturnsArgumentException()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
        passwordValidateMock.SetupGet(v => v.ErrorMessages).Returns(It.IsAny<string>());

        var CreateUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => CreateUser.Create(It.IsAny<string>(), It.IsAny<string>());

        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void Create_InvalidUserRepository_ReturnsException()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Throws(new Exception("Simulando um erro no UserRepository"));
        var emailValidateMock = new Mock<IEmailValidate>();
        emailValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
        var passwordValidateMock = new Mock<IPasswordValidate>();
        passwordValidateMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

        var createUser = new ManagerUser(userRepositoryMock.Object, emailValidateMock.Object, passwordValidateMock.Object);
        Action Act = () => createUser.Create(It.IsAny<string>(), It.IsAny<string>());

        var exception = Assert.Throws<Exception>(Act);
        Assert.Contains(MessageUser.ErrorSave, exception.Message);
    }
}
