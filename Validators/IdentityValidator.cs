using FluentValidation;

namespace DR_Tic_Tac_Toe.Validators
{
    public class IdentityValidator : AbstractValidator<int>
    {
        public IdentityValidator()
        {
            RuleFor(id => id)
                .GreaterThan(0).WithMessage("Id must be greater than 0!");
        }
    }
}
