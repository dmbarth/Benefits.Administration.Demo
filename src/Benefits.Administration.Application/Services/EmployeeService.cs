using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Exceptions;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.Application.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly ILogger<EmployeeService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(ILogger<EmployeeService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _mapper = mapper;
    }

    public async Task<long> AddEmployeeAsync(Employee model)
    {
      _unitOfWork.Employees.Add(model);

      await _unitOfWork.CompleteAsync();

      return model.Id;
    }

    public async Task DeleteEmployeeAsync(long id)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(id);

      if (employee == null)
        new EmployeeNotFoundException();

      _unitOfWork.Employees.Remove(employee);

      await _unitOfWork.CompleteAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(long id)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(id);

      if (employee == null)
        throw new EmployeeNotFoundException();

      return employee;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
      var employees = await _unitOfWork.Employees.GetAllAsync();

      return employees;
    }

    public async Task UpdateEmployeeAsync(long id, Employee model)
    {
      var entity = await _unitOfWork.Employees.GetByIdAsync(id);

      if (entity == null)
        throw new EmployeeNotFoundException();

      _mapper.Map(model, entity);
      
      await _unitOfWork.CompleteAsync();
    }
  }
}