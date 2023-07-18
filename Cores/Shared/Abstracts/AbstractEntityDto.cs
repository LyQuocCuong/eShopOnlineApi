namespace Shared.Abstracts
{
    public abstract class AbstractEntityDto
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
