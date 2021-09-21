using BackEnd.DataLayer.Entities.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DataLayer.Repository
{
    public interface IGenericRepository<TEntity>: IDisposable where TEntity:BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();
        Task<TEntity> GetEntityById(long entityId);
        Task AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        Task RemoveEntity(long entityId);
        void RemoveEntity(TEntity entity);
        Task SaveChanges();

    }
}
