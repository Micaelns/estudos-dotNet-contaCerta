using ContaCerta.Api.Infra.Context;
using ContaCerta.Api.Infra.Costs;
using ContaCerta.Api.Infra.Users;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Costs.Validates;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;
using ContaCerta.Domain.Users.Validates;
using ContaCerta.Domain.Users.Validates.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Api.Configs
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterContexts(this IServiceCollection Services, string? stringConnection)
        {
            Services.AddDbContext<ContaCertaContext>(options =>
                options.UseSqlServer(stringConnection));
            return Services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection Services)
        {
            Services.AddTransient<AddUsers>();
            Services.AddTransient<CreateCost>();
            Services.AddTransient<FindActiveUserByEmail>();
            Services.AddTransient<FindCost>();
            Services.AddTransient<LastCosts>();
            Services.AddTransient<LastCostsCreatedByUser>();
            Services.AddTransient<NextCostsCreatedByUser>();
            Services.AddTransient<ListActivesUsers>();

            return Services;
        }

        public static IServiceCollection ResolveInterfaces(this IServiceCollection Services)
        {
            Services.AddTransient<ICostRepository, CostRepository>();
            Services.AddTransient<IUserRepository, UserRepository>();
            Services.AddTransient<ICostValidate, CostValidate>();
            Services.AddTransient<IEmailValidate, EmailValidate>();
            Services.AddTransient<IUserCostRepository, UserCostRepository>();
            return Services;
        }
    }
}
