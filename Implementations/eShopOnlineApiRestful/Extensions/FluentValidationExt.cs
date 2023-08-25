using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eShopOnlineApiRestful.Extensions
{
    public static class FluentValidationExt
    {
        public static void AddErrorsToModelStateObj(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
