using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using Moq;

namespace ContaCerta.Tests.Domain.Costs.Services
{
    public class ManageUsersTest
    {
        [Fact]
        public void Execute_ValidDataToAddUsers_ReturnsUsersSuccessfully()
        {
            var cost = new Cost("valid_title", "valid_description", 20, DateTime.Now.AddDays(5), new User("valid_email", "valid_password", true), true);
            var users = new User[] { new User("valid_email", "valid_password", true) };
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(new UserCost[] { });
            var addUsers = new ManageUsers(userCostRepositoryMock.Object);
            
            addUsers.Execute(cost, users, []);
            
            userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Once);
        }

        [Fact]
        public void Execute_UserCostsPayedSaved_ReturnsException()
        {
            var cost = new Cost("valid_title", "valid_description", 10, DateTime.Now.AddDays(5), new User("valid_email", "valid_password", true), true);
            var users = new User[] { new User("valid_email", "valid_password", true) };
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            var listUserCost = new UserCost[] {
                                            new UserCost( new User("valid_email", "valid_password", true), cost, 10, payed : true)
                                            };
            userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);

            var addUsers = new ManageUsers(userCostRepositoryMock.Object);

            Action Act = () => addUsers.Execute(cost, users, []);
            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Contains("Após algum pagamento não é possível adicionar usuários", exception.Message);
        }

        [Fact]
        public void Execute_ValidDataToAddUsersWithoutPay_ReturnsUsersSuccessfully()
        {
            var cost = new Cost("valid_title", "valid_description", 10, DateTime.Now.AddDays(5), new User("valid_email", "valid_password", true), true);
            var users = new User[] { new User("valid_email", "valid_password", true) };
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            var listUserCost = new UserCost[] {
                                            new UserCost( new User("valid_email", "valid_password", true), cost, 10, payed : false)
                                            };
            userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
            var addUsers = new ManageUsers(userCostRepositoryMock.Object);
            
            addUsers.Execute(cost, users, []);
            
            userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Once);
        }

        [Fact]
        public void Execute_ValidDataToAddUsers_ReturnsCorrectDivision()
        {
            float individualCostSum = 0;
            float totalCost = 227;
            var cost = new Cost("valid_title", "valid_description", totalCost, DateTime.Now.AddDays(5), new User("valid_email", "valid_password", true), true);
            var user1 = new User("valid_email", "valid_password", true) { Id = 3};
            var user2 = new User("valid_email2", "valid_password", true) { Id = 5 };
            var users = new User[] { user1 , user2 };
            var userCostRepositoryMock = new Mock<IUserCostRepository>();
            float totalCostArrecadar = (float)Math.Round((totalCost / 3), 2)*3;
            var listUserCost = new UserCost[] {
                                            new UserCost( new User("valid_email3", "valid_password", true), cost, 10, payed : false)
                                            };
            userCostRepositoryMock.Setup(x => x.Save(It.IsAny<UserCost>())).Callback<UserCost>( uc => individualCostSum += uc.Value);

            userCostRepositoryMock.Setup(x => x.ListUserCostsByCost(It.IsAny<Cost>())).Returns(listUserCost);
            var addUsers = new ManageUsers(userCostRepositoryMock.Object);
            
            addUsers.Execute(cost, users, []);

            userCostRepositoryMock.Verify(x => x.Save(It.IsAny<UserCost>()), Times.Exactly(3));
            Assert.Equal(totalCostArrecadar, individualCostSum);
        }
    }
}
