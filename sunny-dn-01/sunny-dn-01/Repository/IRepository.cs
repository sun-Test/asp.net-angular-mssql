using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace sunny_dn_01.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        public IEnumerable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(List<TEntity> entities);
    }
}
