using Contracts.Business.Conditions.DeleteEntities.Dictionaries;

namespace Contracts.Business.Conditions.DeleteEntities
{
    public static class DefaultRequiredConditions
    {
        public static readonly List<DeleteCustomerCondition> DeleteACustomer = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteCustomerConditionDictionary.IsExistingInDatabase,
            DeleteCustomerConditionDictionary.IsNotDeletedSoftly
        };

        public static readonly List<DeleteEmployeeCondition> DeleteAnEmployee = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteEmployeeConditionDictionary.IsExistingInDatabase,
            DeleteEmployeeConditionDictionary.IsNotDeletedSoftly,
            DeleteEmployeeConditionDictionary.IsNotAdmin,
            DeleteEmployeeConditionDictionary.IsNotManagerOfStore,
        };

        public static readonly List<DeleteProductCondition> DeleteAProduct = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteProductConditionDictionary.IsExistingInDatabase,
            DeleteProductConditionDictionary.IsNotDeletedSoftly
        };

        public static readonly List<DeleteStoreCondition> DeleteAStore = new()
        {
            // The ORDER of adding ItemCondition is PRIORITY
            DeleteStoreConditionDictionary.IsExistingInDatabase,
            DeleteStoreConditionDictionary.IsNotDeletedSoftly
        };

    }
}
