using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Repositories;
using Benefits.Administration.Infrastructure.DbContexts;
using Benefits.Administration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Benefits.Administration.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContexts(configuration)
                .AddRepositories();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BenefitsDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("BenefitsDbContext"),
                    assembly => assembly.MigrationsAssembly(typeof(BenefitsDbContext).Assembly.FullName)));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IEmployeeRepository, EmployeeRepository>()
                .AddTransient<IBenefitRepository, BenefitRepository>();

            return services;
        }
    }
}
