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
                .HasMany(ben => ben.Discounts)
                .WithOne(dis => dis.Benefit)
                .HasForeignKey(dis => dis.BenefitID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new { ID = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D }
            );
        }
    }
}
