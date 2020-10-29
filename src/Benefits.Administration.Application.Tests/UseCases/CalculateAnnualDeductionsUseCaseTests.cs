using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Models;
using Benefits.Administration.Application.UseCases;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Benefits.Administration.Application.Tests.UseCases
{
    public class CalculateAnnualDeductionsUseCaseTests
    {
        private readonly ILogger<CalculateAnnualDeductionsUseCase> _logger;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CalculateAnnualDeductionsUseCaseTests()
        {
            _logger = Mock.Of<ILogger<CalculateAnnualDeductionsUseCase>>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        private CalculateAnnualDeductionsUseCase UseCase => new CalculateAnnualDeductionsUseCase(_logger, _unitOfWork.Object);

        public ICollection<Dependent> Dependents => new Dependent[] { new Dependent() { ID = 2L, FirstName = "Luke", LastName = "Skywalker", Type = DependentType.Child } };

        public Employee Employee => new Employee() { ID = 1L, FirstName = "Anakin", LastName = "Skywalker", Income = 52000D, Dependents = Dependents };

        public ICollection<Discount> Discounts => new Discount[] { new Discount() { ID = 1L, BenefitID = 1L, Type = DiscountType.NameStartsWithA, Amount = 0.1, IsActive = true } };

        public ICollection<Benefit> Benefits => new Benefit[] {
            new Benefit() { ID = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D, Discounts = Discounts }
        };

        [Fact]
        public void Throws_Exception_When_Benefit_Is_Not_Found()
        {
            // arrange
            _unitOfWork
                .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
                .ReturnsAsync(new List<Benefit>());

            // act
            Func<Task> task = async () => await UseCase.InvokeAsync(123, null);

            // assert
            Assert.ThrowsAsync<Exception>(task);
        }

        [Fact]
        public void Throws_ArgumentException_When_Employee_Is_Not_Found()
        {
            // arrange
            var benefit = new Benefit() { ID = 1L, Year = 2020, PayPeriods = 26, EmployeeCost = 1000D, DependentCost = 500D };
            var benefits = new List<Benefit>() { benefit };

            _unitOfWork
                .Setup(uow => uow.Benefits.FindAsync(It.IsAny<Expression<Func<Benefit, bool>>>()))
                .ReturnsAsync(benefits);

            // act
            Func<Task> task = async () => await UseCase.InvokeAsync(123, null);

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
            var result = await UseCase.InvokeAsync(123, 12);

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
            var result = await UseCase.InvokeAsync(123, 1);

            // assert
            Assert.Equal(Math.Round(900m, 2), Math.Round(result.EmployeeTotal, 2));
            Assert.Equal(Math.Round(500m, 2), Math.Round(result.DependentsTotal, 2));
            Assert.Equal(Math.Round(1400m, 2), Math.Round(result.TotalCost, 2));
        }
    }
}
