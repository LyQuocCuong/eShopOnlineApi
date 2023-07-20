﻿namespace Contracts.Repositories.Entities
{
    public interface IProductRepository
    {
        Product? GetById(bool isTrackChanges, Guid id);

        IEnumerable<Product> GetAll(bool isTrackChanges);

        bool IsValidId(Guid id);

        void Create(Product product);

        void SoftDelete(Product product);

        void HardDelete(Product product);
    }
}
