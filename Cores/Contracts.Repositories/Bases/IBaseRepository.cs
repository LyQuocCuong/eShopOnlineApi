using Domains.Base;
using System.Linq.Expressions;

namespace Contracts.Repositories.Bases
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> FindAll(bool isTrackChanges);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool isTrackChanges);

        // Why is "CreateEntity" instead of "Create" ?
        /// Due to duplication name in implementation
        /// (IBaseRepository and I(Entities)Repository)
        void CreateEntity(TEntity entity);

        void UpdateEntity(TEntity entity);

        // Why don't use "RemoveEntity" name instead of "DeleteEntity"
        //// void RemoveEntity(T entity);
        /// Due to duplication with "Context.Remove()" - EFCore (actually remove).
        /// Our intention just use this method to execute the Soft-Delete.
        void DeleteEntity(TEntity entity);

        // Hard-Delete
        void DeleteEntityPermanently(TEntity obj);
    }
}
