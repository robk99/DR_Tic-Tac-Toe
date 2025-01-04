using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using FluentValidation;

namespace DR_Tic_Tac_Toe.Validators
{
    public class GamesFilteredRequestValidator : AbstractValidator<GamesFilteredRequest>
    {
        public GamesFilteredRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page is required and must be greater than 0.");

            RuleFor(x => x.Size)
                .GreaterThan(0).WithMessage("Size is required and must be greater than 0.");          
        }
    }
}
