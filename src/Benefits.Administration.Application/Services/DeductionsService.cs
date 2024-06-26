using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Exceptions;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Services;
using Benefits.Administration.Application.Models;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.Application.Services
{
  public class DeductionsService : IDeductionsService
  {
    private readonly ILogger<DeductionsService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeductionsService(ILogger<DeductionsService> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<EmployeeDeductions> CalculateDeductionsAsync(long id, int? costPeriods)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(id);

      if (employee == null)
        throw new NotFoundException();

      var benefits = await _unitOfWork.Benefits
        .FindAsync(entity => entity.Year == DateTime.Now.Year);

      var benefit = benefits.FirstOrDefault();

      if (benefit == null)
        throw new Exception("Benefit not found");

      _logger.LogInformation("Benefit retreived");

      _logger.LogInformation("Calculating employee deductions");

      var empCost = (decimal)benefit.EmployeeCost;
      var depCost = (decimal)benefit.DependentCost;

      var discounts = benefit.BenefitDiscounts
        .Where(bd => bd.IsActive)
        .Select(bd => bd.Discount);

      var employeeDiscounts = ApplyDiscounts(discounts, empCost, employee);
      var dependentDiscounts = new List<decimal>();

      foreach (var dependent in employee.Dependents)
        dependentDiscounts.AddRange(ApplyDiscounts(discounts, depCost, dependent));
        
      return new EmployeeDeductions(
        employee.Id,
        costPeriods ?? benefit.PayPeriods,
        empCost,
        employeeDiscounts,
        employee.Dependents.Count * depCost,
        dependentDiscounts
      );
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