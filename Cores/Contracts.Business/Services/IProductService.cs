namespace Contracts.Business.Services
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<bool> IsValidIdAsync(Guid id);

        Task<ProductDto> CreateAsync(ProductForCreationDto creationDto);

        Task<bool> UpdateFullyAsync(Guid id, ProductForUpdateDto updateDto);

        Task<bool> DeleteSoftlyAsync(Guid id);

        Task<bool> DeleteHardAsync(Guid id);
    }
}
