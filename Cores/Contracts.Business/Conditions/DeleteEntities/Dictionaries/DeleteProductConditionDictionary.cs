namespace Contracts.Business.Conditions.DeleteEntities.Dictionaries
{
    public static class DeleteProductConditionDictionary
    {
        public readonly static DeleteProductCondition IsExistingInDatabase = new DeleteProductCondition(
                condition: DeleteProductConditionsEnum.IsExistingInDatabase,
                description: "Product is existing in the Database."
            );

        public readonly static DeleteProductCondition IsNotDeletedSoftly = new DeleteProductCondition(
                condition: DeleteProductConditionsEnum.IsNotDeletedSoftly,
                description: "Product has NOT been deleted softly."
            );
    }
}
