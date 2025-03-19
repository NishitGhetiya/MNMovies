using System.ComponentModel.DataAnnotations;

namespace MNMovies_Backend.Models
{
    public class EpisodeModel
    {
        [Key]
        public int? EpisodeID { get; set; }
        public string? EpisodeName { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeImage { get; set; }
        public string EpisodeVideo { get; set; }
        public string? EpisodeDescription { get; set; }
        public string EpisodeLength { get; set; }
        public DateTime EpisodeDate { get; set; }
        public int SeasonID { get; set; }
        public int? SeasonNumber { get; set; }
        public int SeriesID { get; set; }
        public string? SeriesName { get; set; }
    }
}
