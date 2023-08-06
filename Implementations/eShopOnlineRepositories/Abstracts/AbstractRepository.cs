using Contracts.Repositories.Abstracts;
using Domains.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineRepositories.Abstracts
{
    public abstract class AbstractRepository<TEntity, TEntityRepository> : IAbstractRepository<TEntity> where TEntity : AbstractEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        protected readonly ILogger<TEntityRepository> _logger;

        protected AbstractRepository(ILogger<TEntityRepository> logger, 
                                     RepositoryParams repositoryParams)
        {
            _logger = logger;
            _dbSet = repositoryParams.Context.Set<TEntity>();
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

        public void DeleteEntitySoftly(TEntity entity)
        {
            // Due to "where TEntity : BaseEntity"
            // I can use properties of BaseEntity
            entity.IsDeleted = true;
        }

        public void DeleteEntityHard(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
