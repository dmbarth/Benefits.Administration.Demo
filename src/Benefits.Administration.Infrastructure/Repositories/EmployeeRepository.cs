using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces.Repositories;
using Benefits.Administration.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Benefits.Administration.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BenefitsDbContext context) : base(context) { }

        public async override Task<Employee> GetByIdAsync(long id)
        {
            return await _context.Employees
                .Include(entity => entity.Dependents)
                .SingleOrDefaultAsync(entity => entity.ID == id);
        }
    }
}
