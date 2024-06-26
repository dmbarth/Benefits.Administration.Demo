using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Models;
using Benefits.Administration.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Benefits.Administration.Application.Tests.UseCases
{
  public class DeductionsServiceTests
  {
    private readonly ILogger<DeductionsService> _logger;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public DeductionsServiceTests()
    {
      _logger = Mock.Of<ILogger<DeductionsService>>();
      _unitOfWork = new Mock<IUnitOfWork>();
    }

    private DeductionsService Service => new DeductionsService(_logger, _unitOfWork.Object);

    public ICollection<Dependent> Dependents => new Dependent[] {
      new Dependent() { Id = 2L, FirstName = "Luke", LastName = "Skywalker", Type = DependentType.Child }
    };

    public Employee Employee => new Employee() { Id = 1L, FirstName = "Anakin", LastName = "Skywalker", Income = 52000D, Dependents = Dependents };

    public ICollection<BenefitDiscount> BenefitDiscounts => new BenefitDiscount[] {
      new BenefitDiscount() { Id = 1L, IsActive = true, Discount = new Discount() { Id = 1L, Type = DiscountType.NameStartsWithA, Amount = 0.1 }}
    };

    public ICollection<Benefit> Benefits => new Benefit[] {
        new Benefit() { Id = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D, BenefitDiscounts = BenefitDiscounts }
    };

    [Fact]
    public void Throws_Exception_When_Benefit_Is_Not_Found()
    {
      // arrange
      _unitOfWork
          .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
          .ReturnsAsync(new List<Benefit>());

      // act
      Func<Task> task = async () => await Service.CalculateDeductionsAsync(123, null);

      // assert
      Assert.ThrowsAsync<Exception>(task);
    }

    [Fact]
    public void Throws_ArgumentException_When_Employee_Is_Not_Found()
    {
      // arrange
      var benefit = new Benefit() { Id = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D };
      var benefits = new List<Benefit>() { benefit };

      _unitOfWork
          .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
          .ReturnsAsync(benefits);

      // act
      Func<Task> task = async () => await Service.CalculateDeductionsAsync(123, null);

      // assert
      Assert.ThrowsAsync<ArgumentException>(task);
    }

    [Fact]
    public async Task Returns_Expected_Total_When_PayPeriods_Are_Provided()
    {
      // arrange
      _unitOfWork
          .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
          .ReturnsAsync(Benefits);

      _unitOfWork
          .Setup(uow => uow.Employees.GetByIdAsync(It.IsAny<long>()))
          .ReturnsAsync(Employee);

      // act
      var result = await Service.CalculateDeductionsAsync(123, 12);

      // assert
      Assert.Equal(Math.Round(75.00m, 2), Math.Round(result.EmployeeTotal, 2));
      Assert.Equal(Math.Round(41.67m, 2), Math.Round(result.DependentsTotal, 2));
      Assert.Equal(Math.Round(116.67m, 2), Math.Round(result.TotalCost, 2));
    }

    [Fact]
    public async Task Returns_Expected_Totals_When_Employee_Discount_Is_Applied()
    {
      // arrange
      _unitOfWork
          .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
          .ReturnsAsync(Benefits);

      _unitOfWork
          .Setup(uow => uow.Employees.GetByIdAsync(It.IsAny<long>()))
          .ReturnsAsync(Employee);

      // act
      var result = await Service.CalculateDeductionsAsync(123, 1);

      // assert
      Assert.Equal(Math.Round(900m, 2), Math.Round(result.EmployeeTotal, 2));
      Assert.Equal(Math.Round(500m, 2), Math.Round(result.DependentsTotal, 2));
      Assert.Equal(Math.Round(1400m, 2), Math.Round(result.TotalCost, 2));
    }
  }
}
