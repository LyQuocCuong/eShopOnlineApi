namespace Contracts.Repositories.Conditions.DeleteEntityConditionCollection
{
    public static class DeleteProductConditionCollection
    {
        public readonly static DeleteProductCondition IsExistingInDatabase = new DeleteProductCondition(
                condition: ConditionsForDeletingProduct.IsExistingInDatabase,
                description: "Product is existing in the Database."
            );

        public readonly static DeleteProductCondition IsNotDeletedSoftly = new DeleteProductCondition(
                condition: ConditionsForDeletingProduct.IsNotDeletedSoftly,
                description: "Product has NOT been deleted softly."
            );
    }
}
