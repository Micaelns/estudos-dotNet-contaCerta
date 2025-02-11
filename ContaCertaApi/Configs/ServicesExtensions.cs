using ContaCerta.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Api.Configs;

public static class ServicesExtensions
{
    public static IServiceCollection RegisterContexts(this IServiceCollection Services, string? stringConnection)
    {
        Services.AddDbContext<ContaCertaContext>(options =>
            options.UseSqlServer(stringConnection));
        return Services;
    }
}
