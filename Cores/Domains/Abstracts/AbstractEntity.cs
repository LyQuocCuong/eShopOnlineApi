namespace Domains.Abstracts
{
    public abstract class AbstractEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDateUtcZero { get; set; }

        public DateTime UpdatedDateUtcZero { get; set; }
    }
}
