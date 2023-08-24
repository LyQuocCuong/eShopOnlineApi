namespace Contracts.Business.Conditions.DeleteEntities.Dictionaries
{
    public static class DeleteEmployeeConditionDictionary
    {
        public readonly static DeleteEmployeeCondition IsExistingInDatabase = new DeleteEmployeeCondition(
                condition: DeleteEmployeeConditionsEnum.IsExistingInDatabase,
                description: "Employee is existing in the Database."
            );

        public readonly static DeleteEmployeeCondition IsNotAdmin = new DeleteEmployeeCondition(
                condition: DeleteEmployeeConditionsEnum.IsNotAdminRoot,
                description: "Employee must NOT be the Admin Root."
            );

        public readonly static DeleteEmployeeCondition IsNotDeletedSoftly = new DeleteEmployeeCondition(
                condition: DeleteEmployeeConditionsEnum.IsNotDeletedSoftly,
                description: "Employee has NOT been deleted softly."
            );

        public readonly static DeleteEmployeeCondition IsNotManagerOfStore = new DeleteEmployeeCondition(
                condition: DeleteEmployeeConditionsEnum.IsNotManagerOfStore,
                description: "Employee is NOT a manager of the Store."
            );
    }
}
