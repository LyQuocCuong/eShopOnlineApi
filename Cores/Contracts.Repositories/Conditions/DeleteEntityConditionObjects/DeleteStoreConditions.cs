namespace Contracts.Repositories.Conditions.DeleteEntityConditionObjects
{
    public class DeleteStoreCondition : AbstractDeleteCondition<ConditionsForDeletingStore>
    {
        internal DeleteStoreCondition(ConditionsForDeletingStore condition, string description) 
            : base(condition, description)
        {
        }
    }
}
