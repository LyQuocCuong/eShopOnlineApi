namespace Contracts.Repositories.Conditions.DeleteEntityConditionCollection
{
    public static class DeleteEmployeeConditionCollection
    {
        public readonly static DeleteEmployeeCondition IsExistingInDatabase = new DeleteEmployeeCondition(
                condition: ConditionsForDeletingEmployee.IsExistingInDatabase,
                description: "Employee is existing in the Database."
            );

        public readonly static DeleteEmployeeCondition IsNotAdmin = new DeleteEmployeeCondition(
                condition: ConditionsForDeletingEmployee.IsNotAdminRoot,
                description: "Employee must NOT be the Admin Root."
            );

        public readonly static DeleteEmployeeCondition IsNotDeletedSoftly = new DeleteEmployeeCondition(
                condition: ConditionsForDeletingEmployee.IsNotDeletedSoftly,
                description: "Employee has NOT been deleted softly."
            );

        public readonly static DeleteEmployeeCondition IsNotManagerOfStore = new DeleteEmployeeCondition(
                condition: ConditionsForDeletingEmployee.IsNotManagerOfStore,
                description: "Employee is NOT a manager of the Store."
            );
    }
}
