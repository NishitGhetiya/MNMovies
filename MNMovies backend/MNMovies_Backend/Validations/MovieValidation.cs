using FluentValidation;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Validations
{
    public class MovieValidation : AbstractValidator<MovieModel>
    {
        public MovieValidation()
        {
            RuleFor(m => m.MovieName).NotEmpty().WithMessage("Movie Name Is Required");
            RuleFor(m => m.MovieImage).NotEmpty().WithMessage("Movie Image Is Required");
            RuleFor(m => m.MovieVideo).NotEmpty().WithMessage("Movie Video Is Required");
            RuleFor(m => m.MovieCategory).NotEmpty().WithMessage("Movie Category Is Required");
            RuleFor(m => m.MovieType).NotEmpty().WithMessage("Movie Type Is Required");
            RuleFor(m => m.TypeOfMovie).NotEmpty().WithMessage("Type Of Movie Is Required");
            RuleFor(m => m.MovieLanguage).NotEmpty().WithMessage("Movie Language Is Required");
            RuleFor(m => m.MovieDescription).NotEmpty().WithMessage("Movie Description Is Required");
            RuleFor(m => m.MovieLength).NotEmpty().WithMessage("Movie Length Is Required");
        }
    }
}
