using Redpier.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<IList<TEntity>> GetAllAsync();

        IQueryable<TEntity> Get(int skip, int limit);

        Task<bool> CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
