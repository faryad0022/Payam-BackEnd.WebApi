﻿using BackEnd.DataLayer.Context;
using BackEnd.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Constructor
        private BackEndDbContext context;
        private DbSet<TEntity> dbSet;
        public GenericRepository(BackEndDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();

        }
        #endregion



        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return dbSet.AsQueryable();
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            await dbSet.AddAsync(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            dbSet.Update(entity);
        }

        public void RemoveEntity(TEntity entity)
        {
            entity.IsDelete = true;
            dbSet.Update(entity);
        }

        public async Task RemoveEntity(long entityId)
        {
            var entity = await GetEntityById(entityId);
            RemoveEntity(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }



        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
