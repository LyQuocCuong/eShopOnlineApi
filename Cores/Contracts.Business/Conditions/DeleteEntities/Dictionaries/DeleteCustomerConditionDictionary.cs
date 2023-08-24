namespace Contracts.Business.Conditions.DeleteEntities.Dictionaries
{
    public static class DeleteCustomerConditionDictionary
    {
        public readonly static DeleteCustomerCondition IsExistingInDatabase = new DeleteCustomerCondition
        (
            condition: DeleteCustomerConditionsEnum.IsExistingInDatabase,
            description: "Customer is existing in the Database."
        );

        public readonly static DeleteCustomerCondition IsNotDeletedSoftly = new DeleteCustomerCondition
        (
            condition: DeleteCustomerConditionsEnum.IsNotDeletedSoftly,
            description: "Customer has NOT been deleted softly."
        );
    }
}
