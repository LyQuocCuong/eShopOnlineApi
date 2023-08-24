namespace Contracts.Business.Conditions.DeleteEntities.Enums
{
    public enum DeleteCustomerConditionsEnum
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }

    public enum DeleteEmployeeConditionsEnum
    {
        IsExistingInDatabase,
        IsNotAdminRoot,
        IsNotDeletedSoftly,
        IsNotManagerOfStore
    }

    public enum DeleteProductConditionsEnum
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }

    public enum DeleteStoreConditionsEnum
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }
}
