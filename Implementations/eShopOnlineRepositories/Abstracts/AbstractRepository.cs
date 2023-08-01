using Contracts.Repositories.Abstracts;
using Domains.Abstracts;
using Microsoft.EntityFrameworkCore;
using Shared.Templates;

namespace eShopOnlineRepositories.Abstracts
{
    internal abstract class AbstractRepository<TEntity> : IAbstractRepository<TEntity> where TEntity : AbstractEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        //private readonly ILogService _logService;
        protected abstract string ClassName { get; }

        protected AbstractRepository(RepositoryParams repositoryParams)
        {
            _dbSet = repositoryParams.Context.Set<TEntity>();
            //_logService = repositoryParams.LogService;
        }

        public IQueryable<TEntity> FindAll(bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                //_logService.LogInfo(EFCoreLogMessages.QueryTrackingFindAll);
                return _dbSet;
            }
            //_logService.LogInfo(EFCoreLogMessages.QueryNoTrackingFindAll);
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool isTrackChanges)
        {
            if (isTrackChanges)
            {
                //_logService.LogInfo(EFCoreLogMessages.QueryTracking(expression.Body));
                return _dbSet.Where(expression);
            }
            //_logService.LogInfo(EFCoreLogMessages.QueryNoTracking(expression.Body));
            return _dbSet.Where(expression).AsNoTracking();
        }

        public void CreateEntity(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;
            //_logService.LogInfo(EFCoreLogMessages.Message("Assing NowDate(UTC) to the CreatedDate, UpdatedDate"));

            _dbSet.Add(entity);
            //_logService.LogInfo(EFCoreLogMessages.Create);
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
            //_logService.LogInfo(EFCoreLogMessages.SoftDelete);
        }

        public void DeleteEntityHard(TEntity entity)
        {
            _dbSet.Remove(entity);
            //_logService.LogInfo(EFCoreLogMessages.HardDelete);
        }

        #region LOG FUNCTIONS

        protected void LogMethodInfo(string methodName)
        {
            //_logService.LogInfo(LogContentsTemplate.RepositoryMethodInfo(this.ClassName, methodName));
        }

        protected void LogMethodReturnInfo(string result)
        {
            //_logService.LogInfo(LogContentsTemplate.RepositoryMethodReturn(result));
        }

        private static string FormatContent(string content)
        {
            return LogContentsTemplate.RepositoryFormat(content);
        }

        protected void LogInfo(string message)
        {
            //_logService.LogInfo(FormatContent(message));
        }

        protected void LogError(string message)
        {
            //_logService.LogError(FormatContent(message));
        }

        protected void LogDebug(string message)
        {
            //_logService.LogDebug(FormatContent(message));
        }

        protected void LogWarning(string message)
        {
            //_logService.LogWarning(FormatContent(message));
        }

        #endregion

    }
}
