namespace Domains.Base
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
