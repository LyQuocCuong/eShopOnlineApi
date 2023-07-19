namespace Contracts.Business.Entities
{
    public interface IProductService
    {
        ProductDto? GetById(bool isTrackChanges, Guid id);

        IEnumerable<ProductDto> GetAll(bool isTrackChanges);

        void Create(ProductForCreationDto creationDto);

        void SoftDelete(Guid id);

        void HardDelete(Guid id);
    }
}
