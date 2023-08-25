namespace eShopOnlineApiRestful.FluentValidators.DTOs.UpdateDtos
{
    public sealed class CustomerForUpdateDtoValidator : AbstractValidator<CustomerForUpdateDto>
    {
        public CustomerForUpdateDtoValidator() 
        {
            RuleFor(p => p.Code)
                .NotNull()
                .Length(8); // E2308xxx

            RuleFor(p => p.FirstName)
                .NotNull()
                .MaximumLength(80);

            RuleFor(p => p.LastName)
                .NotNull()
                .MaximumLength(80);

            RuleFor(p => p.Address)
                .NotNull()
                .MaximumLength(200);

            RuleFor(p => p.Phone)
                .NotNull()
                .Matches(@"^\d{10,}$")
                    .WithMessage("Invalid the phone number");
        }
    }
}
