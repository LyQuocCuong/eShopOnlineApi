namespace eShopOnlineRepositories.Entities
{
    public sealed class ProductRepository : AbstractRepository<Product, ProductRepository>, IProductRepository
    {
        internal ProductRepository(ILogger<ProductRepository> logger, 
                                 RepositoryParams repositoryParams) 
            : base(logger, repositoryParams)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync(bool isTrackChanges)
        {
            return await base.FindAll(isTrackChanges).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(bool isTrackChanges, Guid id)
        {
            return await base.FindByCondition(p => p.Id == id, isTrackChanges).FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidIdAsync(Guid id)
        {
            return await base.FindByCondition(p => p.Id == id, isTrackChanges: false).AnyAsync(); ;
        }

        public async Task<Dictionary<DeleteProductCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id)
        {
            _logger.LogInformation(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletionAsync)));
            return await CheckRequiredConditionsForDeletionAsync(id, DefaultDeleteEntityConditions.CheckListForDeletingAProduct);
        }

        public async Task<Dictionary<DeleteProductCondition, bool>> CheckRequiredConditionsForDeletionAsync(Guid id, List<DeleteProductCondition> checkList)
        {
            var result = new Dictionary<DeleteProductCondition, bool>();
            
            DeleteProductCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingProduct.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Product? product = await GetByIdAsync(isTrackChanges: false, id);
                if (product != null)
                {
                    result[prerequisiteCondition] = true;
                    _logger.LogInformation(RepositoryLogs.PassedCondition(prerequisiteCondition.ToString()));

                    // checking Other Conditions
                    foreach (var item in checkList)
                    {
                        result.Add(item, false);
                        switch (item.Condition)
                        {
                            case ConditionsForDeletingProduct.IsNotDeletedSoftly:
                                if (product.IsDeleted == false)
                                {
                                    result[item] = true;
                                }
                                break;
                            default:
                                _logger.LogWarning(RepositoryLogs.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            _logger.LogInformation(RepositoryLogs.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            _logger.LogWarning(RepositoryLogs.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    _logger.LogWarning(RepositoryLogs.ObjectNotExistInDB(nameof(Product), id));
                    _logger.LogWarning(RepositoryLogs.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                _logger.LogError(RepositoryLogs.MissingPrerequisiteCondition(ConditionsForDeletingProduct.IsExistingInDatabase.ToString()));
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
