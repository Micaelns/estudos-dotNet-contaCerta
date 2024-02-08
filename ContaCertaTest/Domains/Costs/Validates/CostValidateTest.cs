using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Validates;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Tests.Domain.Costs.Validates
{
    public class CostValidateTest
    {
        [Fact]
        public void IsValid_ValidDataToCreateCost_ReturnTrue()
        {
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costValid = new Cost(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

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
            string expectedMessageError = "O titulo do custo não pode ser vazio.";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costValid = new Cost(invalidTitle, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

            var costValidate = new CostValidate();
            var isValid = costValidate.IsValid(costValid);

            Assert.False(isValid);
            Assert.Equal(expectedMessageError, costValidate.Messages.First());
        }

        [Fact]
        public void IsValid_InvalidUserOnDataToCreateCost_ReturnFalse()
        {
            string expectedMessageError = "O usuário deve ser válido.";
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            float valueSended = 1;
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = null;
            bool activeSended = true;
            var costValid = new Cost(titleSended, descriptionSended, valueSended, paymentDateSended, userRequestedSended, activeSended);

            var costValidate = new CostValidate();
            var isValid = costValidate.IsValid(costValid);

            Assert.False(isValid);
            Assert.Equal(expectedMessageError, costValidate.Messages.First());
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
            string expectedMessageError = "O valor não pode ser negativo.";
            string titleSended = "valid_title";
            string descriptionSended = "valid_description";
            DateTime paymentDateSended = DateTime.Now.AddDays(5);
            User userRequestedSended = new User("valid_email", "valid_password", true);
            bool activeSended = true;
            var costValid = new Cost(titleSended, descriptionSended, invalidValue, paymentDateSended, userRequestedSended, activeSended);

            var costValidate = new CostValidate();
            var isValid = costValidate.IsValid(costValid);

            Assert.False(isValid);
            Assert.Equal(expectedMessageError, costValidate.Messages.First());
        }
    }
}
