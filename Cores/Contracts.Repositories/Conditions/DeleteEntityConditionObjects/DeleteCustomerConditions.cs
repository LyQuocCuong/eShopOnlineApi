namespace Contracts.Repositories.Conditions.DeleteEntityConditionObjects
{
    public sealed class DeleteCustomerCondition : AbstractDeleteCondition<ConditionsForDeletingCustomer>
    {
        internal DeleteCustomerCondition(ConditionsForDeletingCustomer condition, string description) 
            : base(condition, description)
        {
        }
    }
}
