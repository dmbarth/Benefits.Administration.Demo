using System.Collections.Generic;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;

namespace Benefits.Administration.Application.Interfaces.Services
{
  public interface IBenefitsService 
  {
    public Task<IEnumerable<Benefit>> GetBenefitsAsync();
    public Task<IEnumerable<Benefit>> GetBenefitsAsync(int? year);
    public Task<Benefit> GetBenefitByIdAsync(long id);
  }
}