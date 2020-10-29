using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.UseCases;
using Benefits.Administration.Application.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benefits.Administration.Application.UseCases
{
    public class CalculateAnnualDeductionsUseCase : ICalculateAnnualDeductionsUseCase
    {
        private readonly ILogger<CalculateAnnualDeductionsUseCase> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CalculateAnnualDeductionsUseCase(ILogger<CalculateAnnualDeductionsUseCase> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDeductions> InvokeAsync(long employeeID, int? costPeriods)
        {
            _logger.LogDebug("Calculating employee benefits");

            var benefits = await _unitOfWork.Benefits.FindAsync(entity => entity.Year == DateTime.Now.Year);

            var benefit = benefits.FirstOrDefault();

            if (benefit == null)
                throw new Exception("Benefit not found");

            _logger.LogDebug("Benefit retreived");

            var employee = await _unitOfWork.Employees.GetByIdAsync(employeeID);

            if (employee == null)
                throw new ArgumentException($"Employee not found with ID '{employeeID}'");

            _logger.LogDebug("Employee retreived");

            var empCost = (decimal)benefit.EmployeeCost;
            var depCost = (decimal)benefit.DependentCost;

            var discounts = benefit.Discounts.Where(discount => discount.IsActive);

            var employeeDiscounts = ApplyDiscounts(discounts, empCost, employee);
            var dependentDiscounts = new List<decimal>();

            foreach (var dependent in employee.Dependents)
                dependentDiscounts.AddRange(ApplyDiscounts(discounts, depCost, dependent));

            return new EmployeeDeductions(
                employeeID,
                costPeriods ?? benefit.PayPeriods,
                empCost, 
                employeeDiscounts,
                employee.Dependents.Count * depCost, 
                dependentDiscounts);
        }

        private IEnumerable<decimal> ApplyDiscounts(IEnumerable<Discount> discounts, decimal baseCost, IPerson person)
        {
            var results = discounts.Select(discount =>
            {
                if (discount.Type == DiscountType.NameStartsWithA)
                    return CalculateNameStartsWithADiscount(person, baseCost, (decimal)discount.Amount);

                return 0m;
            });

            return results.Where(result => result > 0);
        }

        private decimal CalculateNameStartsWithADiscount(IPerson person, decimal baseCost, decimal discount)
        {
            var count = 0;

            var names = new string[] { person.FirstName, person.MiddleName, person.LastName }
                .Where(name => string.IsNullOrWhiteSpace(name) == false)
                .ToArray();

            while (count < names.Length)
            {
                var name = names[count++];

                if (name.Substring(0, 1).Equals("A", StringComparison.OrdinalIgnoreCase))
                    return discount * baseCost;
            }

            return 0m;
        }
    }
}
