using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benefits.Administration.Infrastructure.EntityTypeConfigurations
{
  public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
  {
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
      builder.HasData(
        new { Id = 1L, Type = DiscountType.NameStartsWithA, Amount = 0.1 }
      );
    }
  }
}
