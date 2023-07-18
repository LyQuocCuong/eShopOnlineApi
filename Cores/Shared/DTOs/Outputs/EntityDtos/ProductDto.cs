namespace Shared.DTOs.Outputs.EntityDtos
{
    public sealed class ProductDto : AbstractEntityDto
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }
    }
}
