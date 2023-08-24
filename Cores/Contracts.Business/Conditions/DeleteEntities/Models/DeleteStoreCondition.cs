namespace Contracts.Business.Conditions.DeleteEntities.Models
{
    public sealed class DeleteStoreCondition : AbstractDeleteEntityCondition<DeleteStoreConditionsEnum>
    {
        internal DeleteStoreCondition(DeleteStoreConditionsEnum condition, string description)
            : base(condition, description)
        {
        }
    }
}
