using Benefits.Administration.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Benefits.Administration.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        IBenefitRepository Benefits { get; }

        Task<int> CompleteAsync();
    }
}
