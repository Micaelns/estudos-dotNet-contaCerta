using ContaCerta.Api.Context;
using ContaCerta.Domain.Costs.Repositories;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Costs.Validates;
using ContaCerta.Domain.Costs.Validates.Interfaces;
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
            Services.AddTransient<CreateCost>();
            return Services;
        }

        public static IServiceCollection ResolveInterfaces(this IServiceCollection Services)
        {
            Services.AddTransient<ICostRepository, CostRepository>();
            Services.AddTransient<ICostValidate, CostValidate>();
            return Services;
        }
    }
}
