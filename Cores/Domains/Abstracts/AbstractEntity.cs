namespace Domains.Abstracts
{
    public abstract class AbstractEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
