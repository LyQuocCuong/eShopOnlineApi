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

        public Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id)
        {
            LogInfo(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForExecutingWithDefaultCheckList);
            return CheckRequiredConditionsForDeletion(id, CommonVariables.DefaultCheckListOfProductDeletion);
        }

        public Dictionary<ConditionsForDeletingProduct, bool> CheckRequiredConditionsForDeletion(Guid id, List<ConditionsForDeletingProduct> checkList)
        {
            LogInfo(nameof(IsValidId), LogMessages.MessageForStartingMethodExecution);
            Product? product = base.FindByCondition(p => p.Id == id, isTrackChanges: false).FirstOrDefault();
            if (product == null)
            {
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.FormatMessageForObjectWithIdNotExistingInDatabase(nameof(Product), id.ToString()));
                LogWarning(nameof(CheckRequiredConditionsForDeletion), LogMessages.MessageForSettingAllConditionsInCheckListToFalse);
            }
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
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - PASSED. The Product has not been deleted softly yet.");
                                result[condition] = true;
                            }
                            else
                            {
                                LogInfo(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. Because the Product has been deleted softly (IsDeleted = TRUE).");
                            }
                            break;
                        default:
                            LogWarning(nameof(CheckRequiredConditionsForDeletion), $"{condition.ToString()} - FAILED. The checking condition has NOT been implemented yet.");
                            break;
                    }

                }
            }
            LogInfo(nameof(IsValidId), LogMessages.MessageForFinishingMethodExecution);
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
