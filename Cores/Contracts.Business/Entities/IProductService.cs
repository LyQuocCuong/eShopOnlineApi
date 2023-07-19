namespace Contracts.Business.Entities
{
    public interface IProductService
    {
        ProductDto? GetById(Guid id);

        IEnumerable<ProductDto> GetAll();

        void Create(ProductForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
