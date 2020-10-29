using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Repositories;
using Benefits.Administration.Infrastructure.DbContexts;
using Benefits.Administration.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Benefits.Administration.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BenefitsDbContext _context;

        public UnitOfWork(BenefitsDbContext dbContext)
        {
            _context = dbContext;

            Employees = new EmployeeRepository(_context);
            Benefits = new BenefitRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public IBenefitRepository Benefits { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
