using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Costs.Validates;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using ContaCerta.Domain.Users.Validates;
using ContaCerta.Domain.Users.Validates.Interfaces;
using ContaCerta.Infra.Repositories.Costs;
using ContaCerta.Infra.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ContaCerta.Infra;

public static class ServicesExtensions
{

    public static IServiceCollection RegisterServices(this IServiceCollection Services)
    {
        Services.AddTransient<ManagerCost>();
        Services.AddTransient<ManagerUsersInCost>();
        Services.AddTransient<ManagerUser>();
        Services.AddTransient<ListCostsUser>();

        return Services;
    }

    public static IServiceCollection ResolveInterfaces(this IServiceCollection Services)
    {
        Services.AddTransient<ICostRepository, CostRepository>();
        Services.AddTransient<IUserRepository, UserRepository>();
        Services.AddTransient<ICostValidate, CostValidate>();
        Services.AddTransient<IEmailValidate, EmailValidate>();
        Services.AddTransient<IPasswordValidate, PasswordValidate>();
        Services.AddTransient<IUserCostRepository, UserCostRepository>();
        return Services;
    }
}
