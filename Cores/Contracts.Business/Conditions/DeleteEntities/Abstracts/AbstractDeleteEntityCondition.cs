using Contracts.Business.Abstracts;

namespace Contracts.Business.Conditions.DeleteEntities.Abstracts
{
    public abstract class AbstractDeleteEntityCondition<TDeleteEntityCondition> : AbstractCondition<TDeleteEntityCondition> 
        where TDeleteEntityCondition : Enum
    {
        protected AbstractDeleteEntityCondition(TDeleteEntityCondition deleteEntityCondition, string description)
            : base(deleteEntityCondition, description)
        {
        }

        public override string ToString()
        {
            return $"{Condition} - REQUIRED: {Description}";
        }
    }
}
