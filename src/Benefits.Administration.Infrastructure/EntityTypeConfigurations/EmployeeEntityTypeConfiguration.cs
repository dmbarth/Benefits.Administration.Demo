using Benefits.Administration.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benefits.Administration.Infrastructure.EntityTypeConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(emp => emp.Dependents)
                .WithOne(dep => dep.Employee)
                .HasForeignKey(dep => dep.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new { Id = 1L, FirstName = "Anakin", LastName = "Skywalker", Income = 52000D },
                new { Id = 2L, FirstName = "Sarah", MiddleName = "J", LastName = "Connor", Income = 52000D },
                new { Id = 3L, FirstName = "Bruce", MiddleName = "Thomas", LastName = "Wayne", Income = 52000D }
            );
        }
    }
}
