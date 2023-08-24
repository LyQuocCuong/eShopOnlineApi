namespace Contracts.Business.Conditions.DeleteEntities.Models
{
    public sealed class DeleteEmployeeCondition : AbstractDeleteEntityCondition<DeleteEmployeeConditionsEnum>
    {
        internal DeleteEmployeeCondition(DeleteEmployeeConditionsEnum condition, string description)
            : base(condition, description)
        {
        }
    }
}
