using FairDraw.App.Core.Requests;
using FluentValidation;

namespace FairDraw.App.Core.Validators
{
    public class NewCompetitionRequestValidator : AbstractValidator<NewCompetitionRequest>
    {
        public NewCompetitionRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
