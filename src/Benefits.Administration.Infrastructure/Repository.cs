using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Benefits.Administration.Infrastructure
{
  public abstract class Repository<T> : IRepository<T> where T : class
  {
    protected readonly BenefitsDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(BenefitsDbContext dbContext)
    {
      _context = dbContext;
      _dbSet = _context.Set<T>();
    }

    public virtual void Add(T entity)
    {
      _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
      _dbSet.AddRange(entities);
    }

    public async virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
      return await _dbSet.Where(expression).ToListAsync();
    }

    public async virtual Task<IEnumerable<T>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public async virtual Task<T> GetByIdAsync(long id)
    {
      return await _dbSet.FindAsync(id);
    }

    public virtual void Remove(T entity)
    {
      _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
      _dbSet.RemoveRange(entities);
    }
  }
}
