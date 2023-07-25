namespace Contracts.Repositories.Conditions.DefaultConditions
{
    public static class DefaultDeleteEntityConditions
    {
        public static readonly List<DeleteCustomerCondition> CheckListForDeletingACustomer = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteCustomerConditionCollection.IsExistingInDatabase,
            DeleteCustomerConditionCollection.IsNotDeletedSoftly
        };

        public static readonly List<DeleteEmployeeCondition> CheckListForDeletingAnEmployee = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteEmployeeConditionCollection.IsExistingInDatabase,
            DeleteEmployeeConditionCollection.IsNotDeletedSoftly,
            DeleteEmployeeConditionCollection.IsNotAdmin,
            DeleteEmployeeConditionCollection.IsNotManagerOfStore,
        };

        public static readonly List<DeleteProductCondition> CheckListForDeletingAProduct = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteProductConditionCollection.IsExistingInDatabase,
            DeleteProductConditionCollection.IsNotDeletedSoftly
        };

        public static readonly List<DeleteStoreCondition> CheckListForDeletingAStore = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteStoreConditionCollection.IsExistingInDatabase,
            DeleteStoreConditionCollection.IsNotDeletedSoftly
        };

    }
}
