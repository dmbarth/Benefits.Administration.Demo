using Benefits.Administration.Application.Models;
using System.Threading.Tasks;

namespace Benefits.Administration.Application.Interfaces.UseCases
{
    public interface ICalculateAnnualDeductionsUseCase : IUseCase<Task<EmployeeDeductions>, long, int?>
    {
    }
}
