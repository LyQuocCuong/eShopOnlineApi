namespace eShopOnlineRepositories.Entities
{
    internal sealed class ProductRepository : AbstractRepository<Product>, IProductRepository
    {
        protected override string ClassName => nameof(ProductRepository);

        public ProductRepository(RepositoryParams repositoryParams) : base(repositoryParams)
        {
        }

        public IEnumerable<Product> GetAll(bool isTrackChanges)
        {
            LogMethodInfo(nameof(GetAll));
            return base.FindAll(isTrackChanges);
        }

        public Product? GetById(bool isTrackChanges, Guid id)
        {
            LogMethodInfo(nameof(GetById));
            return base.FindByCondition(p => p.Id == id, isTrackChanges).FirstOrDefault();
        }

        public bool IsValidId(Guid id)
        {
            LogMethodInfo(nameof(IsValidId));

            bool result = base.FindByCondition(p => p.Id == id, isTrackChanges: false).Any();

            LogMethodReturnInfo(result.ToString());
            return result;
        }

        public Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletingProduct(Guid id)
        {
            LogMethodInfo(string.Concat("(DEFAULT)", nameof(CheckRequiredConditionsForDeletingProduct)));
            return CheckRequiredConditionsForDeletingProduct(id, DefaultDeleteEntityConditions.CheckListForDeletingAProduct);
        }

        public Dictionary<DeleteProductCondition, bool> CheckRequiredConditionsForDeletingProduct(Guid id, List<DeleteProductCondition> checkList)
        {
            LogMethodInfo(nameof(CheckRequiredConditionsForDeletingProduct));

            var result = new Dictionary<DeleteProductCondition, bool>();
            
            DeleteProductCondition? prerequisiteCondition = checkList.FirstOrDefault(item => item.Condition == ConditionsForDeletingProduct.IsExistingInDatabase);
            if (prerequisiteCondition != null)
            {
                result.Add(prerequisiteCondition, false);
                checkList.Remove(prerequisiteCondition);

                Product? product = base.FindByCondition(p => p.Id == id, isTrackChanges: false).FirstOrDefault();
                if (product != null)
                {
                    result[prerequisiteCondition] = true;
                    LogInfo(RepositoryLogMessages.PassedCondition(prerequisiteCondition.ToString()));

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
                                LogWarning(RepositoryLogMessages.NotImplementedCondition(item.ToString()));
                                break;
                        }
                        if (result[item])
                        {
                            LogInfo(RepositoryLogMessages.PassedCondition(item.ToString()));
                        }
                        else
                        {
                            LogInfo(RepositoryLogMessages.FailedCondition(item.ToString()));
                            break;  // stop checking
                        }
                    }
                }
                else
                {
                    LogInfo(RepositoryLogMessages.ObjectNotExistInDB(nameof(Product), id));
                    LogInfo(RepositoryLogMessages.FailedCondition(prerequisiteCondition.ToString()));
                }
            }
            else
            {
                LogInfo(RepositoryLogMessages.MissingPrerequisiteCondition(ConditionsForDeletingProduct.IsExistingInDatabase.ToString()));
            }
            return result;
        }

        public void Create(Product product)
        {
            LogMethodInfo(nameof(Create));
            base.CreateEntity(product);
        }        

        public void DeleteSoftly(Product product)
        {
            LogMethodInfo(nameof(DeleteSoftly));
            base.DeleteEntitySoftly(product);
        }

        public void DeleteHard(Product product)
        {
            LogMethodInfo(nameof(DeleteHard));
            base.DeleteEntityHard(product);
        }

    }
}
