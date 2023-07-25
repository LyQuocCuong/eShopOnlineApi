namespace Contracts.Repositories.Abstracts
{
    public abstract class AbstractDeleteCondition<TEnumConditions> : AbstractCondition<TEnumConditions>
    {
        protected AbstractDeleteCondition(TEnumConditions condition, string description) 
            : base(condition, description)
        {
        }

        public override string ToString()
        {
            return $"{Condition} - REQUIRED: {Description}";
        }
    }
}
