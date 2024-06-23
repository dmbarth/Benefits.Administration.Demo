using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;

namespace Benefits.Administration.Application.Interfaces.Services
{
  public interface IDependentsService
  {
    Task<Dependent> GetDependentAsync(long employeeId, long dependentId);
    Task<long> AddDependentAsync(long employeeId, Dependent model);
    Task UpdateDependentAsync(long employeeId, long dependentId, Dependent model);
    Task DeleteDependentAsync(long employeeId, long dependentId);
  }
}