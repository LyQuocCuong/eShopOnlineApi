namespace eShopOnlineApiRestful.FluentValidators.DTOs.UpdateDtos
{
    public sealed class CompanyForUpdateDtoValidator : AbstractValidator<CompanyForUpdateDto>
    {
        public CompanyForUpdateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotNull();

            RuleFor(p => p.Address)
                .NotNull();

            RuleFor(p => p.Phone)
                .NotNull()
                .Matches(@"^\d{10,}$").WithMessage("Invalid the phone number");
        }
    }
}
