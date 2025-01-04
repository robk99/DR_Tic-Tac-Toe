using DR_Tic_Tac_Toe.DTOs.Game.Requests;
using FluentValidation;

namespace DR_Tic_Tac_Toe.Validators
{
    public static class ValidatorExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<GamesFilteredRequest>, GamesFilteredRequestValidator>();
            services.AddScoped<IValidator<int>, IdentityValidator>();
            services.AddScoped<IValidator<StartNewGameRequest>, StartNewGameRequestValidator>();
            services.AddScoped<IValidator<NewGameMoveRequest>, NewGameMoveRequestValidator>();

            return services;
        }
    }
}
