using Benefits.Administration.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Benefits.Administration.Infrastructure.DbContexts
{
    public class BenefitsDbContext : DbContext
    {
        public BenefitsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Dependent> Dependents { get; set; }

        public DbSet<Benefit> Benefits { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BenefitsDbContext).Assembly);
        }
    }
}
