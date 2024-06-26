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
  public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IAggregateRoot
  {
    protected readonly BenefitsDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(BenefitsDbContext dbContext)
    {
      _context = dbContext;
      _dbSet = _context.Set<TEntity>();
    }

    public virtual void Add(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
      _dbSet.AddRange(entities);
    }

    public async virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
      return await _dbSet.Where(expression).ToListAsync();
    }

    public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public async virtual Task<TEntity> GetByIdAsync(TId id)
    {
      return await _dbSet.FindAsync(id);
    }

    public virtual void Remove(TEntity entity)
    {
      _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
      _dbSet.RemoveRange(entities);
    }
  }
}
