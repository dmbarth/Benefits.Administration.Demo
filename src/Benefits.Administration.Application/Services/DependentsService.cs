using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Exceptions;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.Application.Services
{
  public class DependentsService : IDependentsService
  {
    private readonly ILogger<DependentsService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DependentsService(ILogger<DependentsService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Dependent> GetDependentAsync(long employeeId, long dependentId)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);

      if (employee == null)
        throw new EmployeeNotFoundException();

      var dependent = employee.Dependents
        .Where(dep => dep.Id == dependentId)
        .FirstOrDefault();

      if (dependent == null)
        throw new DependentNotFoundException();

      return dependent;
    }

    public async Task<long> AddDependentAsync(long employeeId, Dependent model)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);

      if (employee == null)
        throw new EmployeeNotFoundException();

      employee.Dependents.Add(model);

      await _unitOfWork.CompleteAsync();

      return model.Id;
    }

    public async Task DeleteDependentAsync(long employeeId, long dependentId)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);

      if (employee == null)
        throw new EmployeeNotFoundException();

      var dependent = employee.Dependents
        .Where(dep => dep.Id == dependentId)
        .FirstOrDefault();

      if (dependent == null)
        throw new DependentNotFoundException();

      employee.Dependents.Remove(dependent);

      await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateDependentAsync(long employeeId, long dependentId, Dependent model)
    {
      var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);

      if (employee == null)
        throw new EmployeeNotFoundException();

      var dependent = employee.Dependents
        .Where(dep => dep.Id == dependentId)
        .FirstOrDefault();

      if (dependent == null)
        throw new DependentNotFoundException();

      _mapper.Map(model, dependent);

      await _unitOfWork.CompleteAsync();
    }
  }
}