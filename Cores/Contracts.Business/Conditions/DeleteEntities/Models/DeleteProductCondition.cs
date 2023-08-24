namespace Contracts.Business.Conditions.DeleteEntities.Models
{
    public sealed class DeleteProductCondition : AbstractDeleteEntityCondition<DeleteProductConditionsEnum>
    {
        internal DeleteProductCondition(DeleteProductConditionsEnum condition, string description)
            : base(condition, description)
    {
    }
}
}
