using Shared.Common.Enums;

namespace Shared.Common.Variables
{
    public static partial class CommonVariables
    {
        public static readonly List<ConditionsForDeletingCustomer> DefaultCheckListOfCustomerDeletion = new()
        {
            ConditionsForDeletingCustomer.IsNotDeletedSoftly
        };

        public static readonly List<ConditionsForDeletingEmployee> DefaultCheckListOfEmployeeDeletion = new()
        {
            ConditionsForDeletingEmployee.IsNotAdminRoot,
            ConditionsForDeletingEmployee.IsNotDeletedSoftly,
            ConditionsForDeletingEmployee.IsNotManagerOfStore,
        };

        public static readonly List<ConditionsForDeletingProduct> DefaultCheckListOfProductDeletion = new()
        {
            ConditionsForDeletingProduct.IsNotDeletedSoftly
        };

        public static readonly List<ConditionsForDeletingStore> DefaultCheckListOfStoreDeletion = new()
        {
            ConditionsForDeletingStore.IsNotDeletedSoftly
        };

    }
}
