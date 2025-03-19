using System.ComponentModel.DataAnnotations;

namespace MNMovies.Models
{
    public class SeasonModel
    {
        [Key]
        public int? SeasonID { get; set; }
        public int SeasonNumber { get; set; }
        public string SeasonImage { get; set; }
        public string SeasonDescription { get; set; }
        public DateTime SeasonDate { get; set; }
        public int SeriesID { get; set; }
        public string? SeriesName { get; set; }
    }
    public class SeasonCountModel
    {
        public int SeriesID { get; set; }
        public string SeriesName { get; set; }
        public int SeasonCount { get; set; }
    }
}
