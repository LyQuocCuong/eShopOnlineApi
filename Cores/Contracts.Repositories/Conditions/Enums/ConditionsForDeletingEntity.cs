namespace Contracts.Repositories.Conditions.Enums
{
    public enum ConditionsForDeletingCustomer
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }

    public enum ConditionsForDeletingEmployee
    {
        IsExistingInDatabase,
        IsNotAdminRoot,
        IsNotDeletedSoftly,
        IsNotManagerOfStore
    }

    public enum ConditionsForDeletingProduct
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }

    public enum ConditionsForDeletingStore
    {
        IsExistingInDatabase,
        IsNotDeletedSoftly
    }
}
