using System.Threading.Tasks;
using Benefits.Administration.Application.Models;

namespace Benefits.Administration.Application.Interfaces.Services
{
  public interface IDeductionsService
  {
    Task<EmployeeDeductions> CalculateDeductionsAsync(long id, int? costPeriods);
  }
}