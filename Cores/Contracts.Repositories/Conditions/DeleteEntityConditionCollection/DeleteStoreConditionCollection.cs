namespace Contracts.Repositories.Conditions.DeleteEntityConditionCollection
{
    public static class DeleteStoreConditionCollection
    {
        public readonly static DeleteStoreCondition IsExistingInDatabase = new DeleteStoreCondition(
                condition: ConditionsForDeletingStore.IsExistingInDatabase,
                description: "Store is existing in the Database."
            );

        public readonly static DeleteStoreCondition IsNotDeletedSoftly = new DeleteStoreCondition(
                condition: ConditionsForDeletingStore.IsNotDeletedSoftly,
                description: "Store has NOT been deleted softly."
            );
    }
}
