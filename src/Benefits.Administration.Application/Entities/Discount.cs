using Benefits.Administration.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Benefits.Administration.Application.Entities
{
    public class Discount : Entity
    {
        protected internal Discount() { }

        [Required]
        public DiscountType Type { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long BenefitID { get; set; }

        [JsonIgnore]
        public Benefit Benefit { get; set; }
    }
}
