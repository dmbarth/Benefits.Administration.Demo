using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benefits.Administration.Infrastructure.EntityTypeConfigurations
{
    public class DependentEntiityTypeConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.HasKey(entity => entity.ID);

            builder.Property(entity => entity.ID)
                .ValueGeneratedOnAdd();

            builder.HasData(
                new { ID = 1L, FirstName = "Padme", LastName = "Skywalker", Type = DependentType.Spouse, EmployeeID = 1L },
                new { ID = 2L, FirstName = "Luke", LastName = "Skywalker", Type = DependentType.Child, EmployeeID = 1L },
                new { ID = 3L, FirstName = "Leah", LastName = "Skywalker", Type = DependentType.Child, EmployeeID = 1L },
                new { ID = 4L, FirstName = "John", LastName = "Connor", Type = DependentType.Child, EmployeeID = 2L }
            );
        }
    }
}
