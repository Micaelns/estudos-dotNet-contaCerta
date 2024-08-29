using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Test.Mocks;

public class CostMock
{
    public static Cost Generate(User user)
    {
        return new Cost("valid_title", "valid_description", 100, DateTime.Now.AddDays(5), user);
    }

    public static Cost Generate()
    {
        return CostMock.Generate(UserMock.Generate());
    }
}
