using Domains.Abstracts;
using System.Linq.Expressions;

namespace Contracts.Repositories.Abstracts
{
    public interface IAbstractRepository<TEntity> where TEntity : AbstractEntity
    {
        IQueryable<TEntity> FindAll(bool isTrackChanges);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool isTrackChanges);

        // Why is "CreateEntity" instead of "Create" ?
        /// Due to duplication name in implementation
        /// (IAbstractRepository and I(Entities)Repository)
        void CreateEntity(TEntity entity);

        void UpdateEntity(TEntity entity);

        // Why don't use "RemoveEntity" name instead of "SoftDeleteEntity"
        //// void RemoveEntity(T entity);
        /// Due to duplication with "Context.Remove()" - EFCore (actually remove - HardDelete).
        /// Our intention just use this method to execute the Soft-Delete (Entity.IsDeleted = true).
        void SoftDeleteEntity(TEntity entity);

        void HardDeleteEntity(TEntity entity);
    }
}
