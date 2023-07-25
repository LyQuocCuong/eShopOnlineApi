namespace Contracts.Repositories.Conditions.DeleteEntityConditionObjects
{
    public class DeleteProductCondition : AbstractDeleteCondition<ConditionsForDeletingProduct>
    {
        internal DeleteProductCondition(ConditionsForDeletingProduct condition, string description) 
            : base(condition, description)
        {
        }
    }
}
