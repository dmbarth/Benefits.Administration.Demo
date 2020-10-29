using AutoMapper;
using Benefits.Administration.Application.Interfaces.UseCases;
using Benefits.Administration.Application.Models.Profiles;
using Benefits.Administration.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Benefits.Administration.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
            services.AddUseCases();

            return services;
        }

        private static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICalculateAnnualDeductionsUseCase, CalculateAnnualDeductionsUseCase>();

            return services;
        }
    }
}
