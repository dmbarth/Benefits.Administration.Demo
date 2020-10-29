using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Benefits.Administration.Application.Entities
{
    public class Dependent : Entity, IPerson
    {
        protected internal Dependent() { }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DependentType Type { get; set; }

        [Required]
        public long EmployeeID { get; protected set; }

        [JsonIgnore]
        public Employee Employee { get; protected set; }
    }
}
