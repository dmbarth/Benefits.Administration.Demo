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
      builder.HasKey(entity => entity.Id);

      builder.Property(entity => entity.Id)
          .ValueGeneratedOnAdd();

      builder.HasData(
        new { Id = 1L, FirstName = "Padme", LastName = "Skywalker", Type = DependentType.Spouse, EmployeeId = 1L },
        new { Id = 2L, FirstName = "Luke", LastName = "Skywalker", Type = DependentType.Child, EmployeeId = 1L },
        new { Id = 3L, FirstName = "Leah", LastName = "Skywalker", Type = DependentType.Child, EmployeeId = 1L },
        new { Id = 4L, FirstName = "John", LastName = "Connor", Type = DependentType.Child, EmployeeId = 2L }
      );
    }
  }
}
