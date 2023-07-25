namespace Contracts.Repositories.Conditions.DeleteEntityConditionCollection
{
    public static class DeleteCustomerConditionCollection
    {
        public readonly static DeleteCustomerCondition IsExistingInDatabase = new DeleteCustomerCondition(
                condition: ConditionsForDeletingCustomer.IsExistingInDatabase,
                description: "Customer is existing in the Database."
            );

        public readonly static DeleteCustomerCondition IsNotDeletedSoftly = new DeleteCustomerCondition(
                condition: ConditionsForDeletingCustomer.IsNotDeletedSoftly,
                description: "Customer has NOT been deleted softly."
            );
    }
}
