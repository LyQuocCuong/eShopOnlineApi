namespace Contracts.Business.Conditions.DeleteEntities.Dictionaries
{
    public static class DeleteStoreConditionDictionary
    {
        public readonly static DeleteStoreCondition IsExistingInDatabase = new DeleteStoreCondition(
                condition: DeleteStoreConditionsEnum.IsExistingInDatabase,
                description: "Store is existing in the Database."
            );

        public readonly static DeleteStoreCondition IsNotDeletedSoftly = new DeleteStoreCondition(
                condition: DeleteStoreConditionsEnum.IsNotDeletedSoftly,
                description: "Store has NOT been deleted softly."
            );
    }
}
