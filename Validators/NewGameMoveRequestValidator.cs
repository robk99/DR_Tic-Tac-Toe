using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using FluentValidation;

namespace DR_Tic_Tac_Toe.Validators
{
    public class NewGameMoveRequestValidator : AbstractValidator<NewGameMoveRequest>
    {
        public NewGameMoveRequestValidator()
        {
            RuleFor(x => x.Field)
                .GreaterThan(0)
                .LessThan(10)
                .WithMessage("Field is required and must be a number from 1 to 9.");     
        }
    }
}
