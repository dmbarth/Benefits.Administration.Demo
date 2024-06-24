using Benefits.Administration.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Benefits.Administration.Application.Entities
{
  public class Discount : Entity<long>
  {
    protected internal Discount() { }

    [Required]
    public DiscountType Type { get; set; }

    [Required]
    public double Amount { get; set; }

    [JsonIgnore]
    public virtual ICollection<Benefit> Benefits { get; set; }

    [JsonIgnore]
    public virtual ICollection<BenefitDiscount> BenefitDiscounts { get; set; }
  }
}
