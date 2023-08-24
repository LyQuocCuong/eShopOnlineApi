namespace Contracts.Business.Abstracts
{
    public abstract class AbstractCondition<TCondition>
        where TCondition : Enum
    {
        public readonly TCondition Condition;

        public readonly string Description;

        protected AbstractCondition(TCondition condition, string description)
        {
            Condition = condition;
            Description = description;
        }

    }
}
