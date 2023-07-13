namespace Domains.Entities
{
    public sealed class Product : BaseEntity
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

    }
}
