using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Interfaces.Repositories;
using Benefits.Administration.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Benefits.Administration.Infrastructure.Repositories
{
    public class BenefitRepository : Repository<Benefit>, IBenefitRepository
    {
        public BenefitRepository(BenefitsDbContext context) : base(context) { }

        public async override Task<Benefit> GetByIdAsync(long id)
        {
            return await _context.Benefits
                .Where(entity => entity.ID == id)
                .Include(entity => entity.Discounts)
                .FirstOrDefaultAsync();
        }

        public async override Task<IEnumerable<Benefit>> FindAsync(Expression<Func<Benefit, bool>> expression)
        {
            return await _context.Benefits
                .Where(expression)
                .Include(entity => entity.Discounts)
                .ToListAsync();
        }
    }
}
