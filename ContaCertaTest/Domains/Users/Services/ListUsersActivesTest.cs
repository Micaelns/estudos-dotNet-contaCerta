using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using Moq;

namespace ContaCerta.Tests.Domain.Users.Services
{
    public class ListUsersActivesTest
    {
        [Fact]
        public void Execute_ReturnsArrayExistingUsers()
        {
            var activesUsers = new[] { 
                new User("email1@mail.com", "********", true), 
                new User("email2@mail.com", "********", true), 
                new User("email3@mail.com", "********", true) 
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.ListActives()).Returns(activesUsers);

            var usersActives = new ListActivesUsers(userRepositoryMock.Object);
            var usersList = usersActives.Execute();

            Assert.Equal(activesUsers.Length, usersList.Length);
        }

        [Fact]
        public void Execute_ReturnsEmpyWhenNoExistUsers()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.ListActives()).Returns(Array.Empty<User>());

            var usersActives = new ListActivesUsers(userRepositoryMock.Object);
            var usersList = usersActives.Execute();

            Assert.Empty(usersList);
        }
    }
}