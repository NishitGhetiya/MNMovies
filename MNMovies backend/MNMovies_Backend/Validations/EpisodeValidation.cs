using FluentValidation;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Validations
{
    public class EpisodeValidation : AbstractValidator<EpisodeModel>
    {
        public EpisodeValidation()
        {
            RuleFor(e => e.EpisodeNumber).NotEmpty().WithMessage("Episode Number Is Required");
            RuleFor(e => e.EpisodeImage).NotEmpty().WithMessage("Episode Image Is Required");
            RuleFor(e => e.EpisodeVideo).NotEmpty().WithMessage("Episode Video Is Required");
            RuleFor(e => e.EpisodeLength).NotEmpty().WithMessage("Episode Length Is Required");
            RuleFor(e => e.SeasonID).NotEmpty().WithMessage("Season ID Is Required");
            RuleFor(e => e.SeriesID).NotEmpty().WithMessage("Series ID Is Required");
        }
    }
}
