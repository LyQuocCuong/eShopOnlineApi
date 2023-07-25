namespace Contracts.Repositories.Abstracts
{
    public abstract class AbstractCondition<TEnumConditions>
    {
        public readonly TEnumConditions Condition;

        public readonly string Description;

        protected AbstractCondition(TEnumConditions condition, string description)
        {
            this.Condition = condition;
            this.Description = description;
        }

    }
}
