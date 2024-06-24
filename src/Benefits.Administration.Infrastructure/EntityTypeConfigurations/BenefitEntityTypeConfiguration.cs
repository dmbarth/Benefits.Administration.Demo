using Benefits.Administration.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benefits.Administration.Infrastructure.EntityTypeConfigurations
{
  public class BenefitEntityTypeConfiguration : IEntityTypeConfiguration<Benefit>
  {
    public void Configure(EntityTypeBuilder<Benefit> builder)
    {
      builder
        .HasMany(entity => entity.Discounts)
        .WithMany(entity => entity.Benefits)
        .UsingEntity<BenefitDiscount>();

      builder.HasData(
        new { Id = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D }
      );
    }
  }
}
