using Benefits.Administration.Application.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Benefits.Administration.Application.Entities
{
    public class Employee : Entity, IPerson, IAggregateRoot
    {
        protected internal Employee() { }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public double Income { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
    }
}
