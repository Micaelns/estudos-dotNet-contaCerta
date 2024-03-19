using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Api.Infra.Context
{
    public class ContaCertaContext : DbContext
    {
        public ContaCertaContext(DbContextOptions<ContaCertaContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<UserCost> UserCosts { get; set; }
    }
}
