using Benefits.Administration.Application.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Benefits.Administration.Application.Entities
{
    public class Benefit : Entity, IAggregateRoot
    {
        protected internal Benefit() { }

        [Required, MaxLength(4)]
        public int Year { get; set; }

        [Required, MaxLength(2)]
        public int PayPeriods { get; set; }

        [Required]
        public double EmployeeCost { get; set; }

        [Required]
        public double DependentCost { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
