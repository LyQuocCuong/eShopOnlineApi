namespace eShopOnlineRepositories.Entities
{
    internal sealed class ProductRepository : AbstractRepository<Product>, IProductRepository
    {
        protected override string ChildClassName => nameof(ProductRepository);

        public ProductRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Product> GetAll(bool isTrackChanges)
        {
            LogInfo(nameof(GetAll), LogMessages.MessageForExecutingMethod);
            return base.FindAll(isTrackChanges);
        }

        public Product? GetById(bool isTrackChanges, Guid id)
        {
            LogInfo(nameof(GetById), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(p => p.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForExecutingMethod);
            return base.FindByCondition(p => p.Id == id, isTrackChanges: false).Any();
        }

        public Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, DefaultDeleteEntityConditions.CheckListForDeletingAProduct);
        }

        public Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletion(Guid id, List<DeleteProductCondition> checkList)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingMethod);

            var result = new Dictionary<DeleteProductCondition, bool>();
            
            Product? product = base.FindByCondition(p => p.Id == id, isTrackChanges: false).FirstOrDefault();
            DeleteProductCondition? isExistingInDatabaseCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingProduct.IsExistingInDatabase);

            if (isExistingInDatabaseCondition != null)
            {
                result.Add(isExistingInDatabaseCondition, false);
                checkList.Remove(isExistingInDatabaseCondition);
                if (product != null)
                {
                    result[isExistingInDatabaseCondition] = true;
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(isExistingInDatabaseCondition.ToString()));

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
                                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(LogMessages.MessageForNotImplementedCondition));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectPassed(item.ToString()));
                        }
                        else
                        {
                            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                    LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed(isExistingInDatabaseCondition.ToString()));
                }
            }
            else
            {
                LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectFailed("Missing IsExistingInDatabase."));
            }
            return result;
        }

        public void Create(Product product)
        {
            LogInfo(nameof(Create), LogMessages.MessageForExecutingMethod);
            base.CreateEntity(product);
        }        

        public void DeleteSoftly(Product product)
        {
            LogInfo(nameof(DeleteSoftly), LogMessages.MessageForExecutingMethod);
            base.DeleteEntitySoftly(product);
        }

        public void DeleteHard(Product product)
        {
            LogInfo(nameof(DeleteHard), LogMessages.MessageForExecutingMethod);
            base.DeleteEntityHard(product);
        }

    }
}
