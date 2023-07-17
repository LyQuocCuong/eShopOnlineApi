﻿using Contracts.Repositories.Abstracts;
using Domains.Base;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineRepositories.Abstracts
{
    internal abstract class AbstractRepository<TEntity> : IAbstractRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        protected AbstractRepository(ShopOnlineContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAll(bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                return _dbSet;
            }
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                return _dbSet.Where(expression);
            }
            return _dbSet.Where(expression).AsNoTracking();
        }

        public void CreateEntity(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;
            
            _dbSet.Add(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            // Temporary Empty
            // Because I can update Entity by using Tracking feature.
        }

        public void SoftDeleteEntity(TEntity entity)
        {
            // Due to "where TEntity : BaseEntity"
            // I can use properties of BaseEntity
            entity.IsRemoved = true;
        }

        public void HardDeleteEntity(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}