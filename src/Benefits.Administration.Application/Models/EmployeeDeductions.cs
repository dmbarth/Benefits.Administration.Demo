using System.Collections.Generic;
using System.Linq;

namespace Benefits.Administration.Application.Models
{
    public class EmployeeDeductions
    {
        private readonly decimal _employeeCost;
        private readonly IEnumerable<decimal> _employeeDiscounts;
        private readonly decimal _dependentsCost;
        private readonly IEnumerable<decimal> _dependentsDiscounts;

        public EmployeeDeductions(
            long employeeID, int costPeriods,
            decimal employeeCost, IEnumerable<decimal> employeeDiscounts,
            decimal dependentsCost, IEnumerable<decimal> dependentsDiscounts)
        {
            EmployeeID = employeeID;
            CostPeriods = costPeriods;

            _employeeCost = employeeCost;
            _employeeDiscounts = employeeDiscounts;
            _dependentsCost = dependentsCost;
            _dependentsDiscounts = dependentsDiscounts;
        }

        public long EmployeeID { get; }

        public int CostPeriods { get; }

        public decimal EmployeeCost => _employeeCost / CostPeriods;

        public IEnumerable<decimal> EmployeeDiscounts => _employeeDiscounts.Select(discount => discount / CostPeriods);

        public decimal EmployeeTotal => EmployeeCost - EmployeeDiscounts.Sum();

        public decimal DependentsCost => _dependentsCost / CostPeriods;

        public IEnumerable<decimal> DependentsDiscounts => _dependentsDiscounts.Select(discount => discount / CostPeriods);

        public decimal DependentsTotal => DependentsCost - DependentsDiscounts.Sum();

        public decimal TotalCost => EmployeeTotal + DependentsTotal;
    }
}
