using System.Collections.Generic;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;

namespace Benefits.Administration.Application.Interfaces.Services
{
  public interface IEmployeeService
  {
    public Task<IEnumerable<Employee>> GetEmployeesAsync();
    public Task<Employee> GetEmployeeByIdAsync(long id);
    public Task<long> AddEmployeeAsync(Employee model);
    public Task UpdateEmployeeAsync(long id, Employee model);
    public Task DeleteEmployeeAsync(long id);
  }
}