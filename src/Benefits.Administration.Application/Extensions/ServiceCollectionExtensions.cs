using System.Reflection;
using AutoMapper;
using Benefits.Administration.Application.Interfaces.Services;
using Benefits.Administration.Application.Models.Profiles;
using Benefits.Administration.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Benefits.Administration.Application.Extensions
{
  public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
          services
            .AddScoped<IBenefitsService, BenefitsService>()
            .AddScoped<IDeductionsService, DeductionsService>()
            .AddScoped<IDependentsService, DependentsService>()
            .AddScoped<IEmployeeService, EmployeeService>();

          return services;
        }
    }
}
