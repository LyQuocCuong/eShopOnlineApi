namespace Shared.Abstracts
{
    public abstract class AbstractEntityDto
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDateUtcZero { get; set; }

        public DateTime UpdatedDateUtcZero { get; set; }
    }
}
