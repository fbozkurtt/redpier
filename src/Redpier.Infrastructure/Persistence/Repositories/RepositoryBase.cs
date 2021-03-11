using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Common;
using Redpier.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);

            return await _dbContext.SaveChangesAsync() > 1;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().SingleOrDefaultAsync(w => w.Id == id);

            _dbContext.Set<TEntity>().Remove(entity);

            return await _dbContext.SaveChangesAsync() > 1;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> Get(int skip, int limit)
        {
            return _dbContext.Set<TEntity>()
            .OrderByDescending(w => w.Created)
            .Skip(skip)
            .Take(limit);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>()
                .Update(entity);

            return await _dbContext.SaveChangesAsync() > 1;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            return await _dbContext.SaveChangesAsync() > 1;
        }
    }
}
