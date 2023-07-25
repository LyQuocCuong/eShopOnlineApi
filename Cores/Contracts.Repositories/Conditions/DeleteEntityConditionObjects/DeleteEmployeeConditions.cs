namespace Contracts.Repositories.Conditions.DeleteEntityConditionObjects
{
    public class DeleteEmployeeCondition : AbstractDeleteCondition<ConditionsForDeletingEmployee>
    {
        internal DeleteEmployeeCondition(ConditionsForDeletingEmployee condition, string description) 
            : base(condition, description)
        {
        }
    }
}
