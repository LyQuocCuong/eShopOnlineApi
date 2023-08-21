namespace Contracts.Business.Entities
{
    public interface IStoreService
    {
        Task<StoreDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<StoreDto>> GetAllAsync();

        Task<bool> IsValidIdAsync(Guid id);

        Task<StoreDto> CreateAsync(StoreForCreationDto creationDto);

        Task<bool> UpdateFullyAsync(Guid id, StoreForUpdateDto updateDto);

        Task<bool> DeleteSoftlyAsync(Guid id);

        Task<bool> DeleteHardAsync(Guid id);
    }
}
