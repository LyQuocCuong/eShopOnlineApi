namespace Shared.Common.Enums
{
    public enum ConditionsForDeletingCustomer
    {
        IsNotDeletedSoftly
    }

    public enum ConditionsForDeletingEmployee
    {
        IsNotAdminRoot,
        IsNotDeletedSoftly,
        IsNotManagerOfStore
    }

    public enum ConditionsForDeletingProduct
    {
        IsNotDeletedSoftly
    }

    public enum ConditionsForDeletingStore
    {
        IsNotDeletedSoftly
    }

}
