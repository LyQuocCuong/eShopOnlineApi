namespace Contracts.Business.Entities
{
    public interface IProductService
    {
        ProductDto? GetById(Guid id);

        IEnumerable<ProductDto> GetAll();

        bool IsValidId(Guid id);

        ProductDto Create(ProductForCreationDto creationDto);

        bool UpdateFully(Guid id, ProductForUpdateDto updateDto);

        bool DeleteSoftly(Guid id);

        bool DeleteHard(Guid id);
    }
}
