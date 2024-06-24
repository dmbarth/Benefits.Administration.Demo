using Benefits.Administration.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Benefits.Administration.Application.Entities
{
  public class BenefitDiscount : Entity<long>, IAggregateRoot
  {
    protected internal BenefitDiscount() { }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public long BenefitId { get; set; }

    [Required]
    public long DiscountId { get; set; }

    public Benefit Benefit { get; set; }
    public Discount Discount { get; set; }
  }
}
