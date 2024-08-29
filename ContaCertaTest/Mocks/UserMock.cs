using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Test.Mocks;

public class UserMock
{
    public static User Generate()
    {
        return new User("valid_email", "valid_password", true);
    }
}
