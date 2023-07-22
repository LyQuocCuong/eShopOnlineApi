using Contracts.Repositories.Abstracts;
using Domains.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace eShopOnlineRepositories.Abstracts
{
    internal abstract class AbstractRepository<TEntity> : IAbstractRepository<TEntity> where TEntity : AbstractEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogService _logService;
        protected abstract string ChildClassName { get; }

        protected AbstractRepository(RepositoryParams repositoryParams)
        {
            _dbSet = repositoryParams.Context.Set<TEntity>();
            _logService = repositoryParams.LogService;
        }

        public IQueryable<TEntity> FindAll(bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                _logService.LogInfo(LogMessages.FormatMessageForEFCore($"[TRACKING] Executing {nameof(FindAll)}() method"));
                return _dbSet;
            }
            _logService.LogInfo(LogMessages.FormatMessageForEFCore($"[NO-TRACKING] Executing {nameof(FindAll)}() method"));
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                _logService.LogInfo(LogMessages.FormatMessageForEFCore($"[TRACKING] Executing {nameof(FindByCondition)}() method"));
                return _dbSet.Where(expression);
            }
            _logService.LogInfo(LogMessages.FormatMessageForEFCore($"[NO-TRACKING] Executing {nameof(FindByCondition)}() method"));
            return _dbSet.Where(expression).AsNoTracking();
        }

        public void CreateEntity(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;
            _logService.LogInfo(LogMessages.FormatMessageForEFCore("[CREATE] Assing NowDate(UTC) to the CreatedDate, UpdatedDate"));

            _dbSet.Add(entity);
            _logService.LogInfo(LogMessages.FormatMessageForEFCore("[CREATE] Change EntityState"));

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
            _logService.LogInfo(LogMessages.FormatMessageForEFCore("[SOFT-DELETE] Set (IsDeleted) to TRUE"));
        }

        public void DeleteEntityHard(TEntity entity)
        {
            _dbSet.Remove(entity);
            _logService.LogInfo(LogMessages.FormatMessageForEFCore("[HARD-DELETE] Change EntityState"));
        }

        public void LogDebug(string methodName, string message)
        {
            _logService.LogDebug(LogMessages.FormatMessageForRepository(ChildClassName, methodName, message));
        }

        public void LogError(string methodName, string message)
        {
            _logService.LogError(LogMessages.FormatMessageForRepository(ChildClassName, methodName, message));
        }

        public void LogInfo(string methodName, string message)
        {
            _logService.LogInfo(LogMessages.FormatMessageForRepository(ChildClassName, methodName, message));
        }

        public void LogWarning(string methodName, string message)
        {
            _logService.LogWarning(LogMessages.FormatMessageForRepository(ChildClassName, methodName, message));
        }
    }
}
