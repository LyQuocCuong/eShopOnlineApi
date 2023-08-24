namespace Contracts.Business.Conditions.DeleteEntities.Models
{
    public sealed class DeleteCustomerCondition : AbstractDeleteEntityCondition<DeleteCustomerConditionsEnum>
    {
        internal DeleteCustomerCondition(DeleteCustomerConditionsEnum condition, string description)
            : base(condition, description)
        {
        }
    }
}
