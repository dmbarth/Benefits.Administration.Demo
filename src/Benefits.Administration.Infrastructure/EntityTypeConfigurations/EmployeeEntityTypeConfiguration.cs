using Benefits.Administration.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benefits.Administration.Infrastructure.EntityTypeConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(entity => entity.ID);

            builder.Property(entity => entity.ID)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(emp => emp.Dependents)
                .WithOne(dep => dep.Employee)
                .HasForeignKey(dep => dep.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new { ID = 1L, FirstName = "Anakin", LastName = "Skywalker", Income = 52000D },
                new { ID = 2L, FirstName = "Sarah", MiddleName = "J", LastName = "Connor", Income = 52000D },
                new { ID = 3L, FirstName = "Bruce", MiddleName = "Thomas", LastName = "Wayne", Income = 52000D }
            );
        }
    }
}
