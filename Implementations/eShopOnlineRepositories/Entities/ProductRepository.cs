namespace eShopOnlineRepositories.Entities
{
    internal sealed class ProductRepository : AbstractRepository<Product>, IProductRepository
    {
        public ProductRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Product> GetAll(bool isTrackChanges)
        {
            return base.FindAll(isTrackChanges);
        }

        public Product? GetById(bool isTrackChanges, Guid id)
        {
            return base.FindByCondition(p => p.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            return base.FindByCondition(p => p.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfProductDeletion);
        }

        public Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingProduct> checkList)
        {
            Product? product = base.FindByCondition(p => p.Id == id, isTrackChanges: false).FirstOrDefault();
            var result = new Dictionary<ConditionsForDeletingProduct, bool>();
            foreach (var condition in checkList)
            {
                result.Add(condition, false);
                if (product != null)
                {
                    switch (condition)
                    {
                        case ConditionsForDeletingProduct.IsNotDeletedSoftly:
                            if (product.IsDeleted == false)
                            {
                                result[condition] = true;
                            }
                            break;
                    }

                }
            }
            return result;
        }

        public void Create(Product product)
        {
            base.CreateEntity(product);
        }        

        public void DeleteSoftly(Product product)
        {
            base.DeleteEntitySoftly(product);
        }

        public void DeleteHard(Product product)
        {
            base.DeleteEntityHard(product);
        }

    }
}
