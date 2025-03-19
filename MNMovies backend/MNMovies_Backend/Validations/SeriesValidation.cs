using FluentValidation;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Validations
{
    public class SeriesValidation : AbstractValidator<SeriesModel>
    {
        public SeriesValidation()
        {
            RuleFor(s => s.SeriesName).NotEmpty().WithMessage("Series Name Is Required");
            RuleFor(s => s.SeriesImage).NotEmpty().WithMessage("Series Image Is Required");
            RuleFor(s => s.TypeOfSeries).NotEmpty().WithMessage("Type Of Series Is Required");
            RuleFor(s => s.SeriesCategory).NotEmpty().WithMessage("Series Category Is Required");
            RuleFor(s => s.SeriesType).NotEmpty().WithMessage("Series Type Is Required");
            RuleFor(s => s.SeriesLanguage).NotEmpty().WithMessage("Series Language Is Required");
            RuleFor(s => s.SeriesDescription).NotEmpty().WithMessage("Series Description Is Required");
        }
    }
}
