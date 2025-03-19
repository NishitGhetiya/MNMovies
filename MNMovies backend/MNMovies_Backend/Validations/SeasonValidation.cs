using FluentValidation;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Validations
{
    public class SeasonValidation : AbstractValidator<SeasonModel>
    {
        public SeasonValidation()
        {
            RuleFor(s => s.SeasonNumber).NotEmpty().WithMessage("Season Number Is Required");
            RuleFor(s => s.SeasonImage).NotEmpty().WithMessage("Season Image Is Required");
            RuleFor(s => s.SeasonDescription).NotEmpty().WithMessage("Season Description Is Required");
            RuleFor(s => s.SeriesID).NotEmpty().WithMessage("Series ID Is Required");
        }
    }
}