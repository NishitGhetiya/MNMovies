using System.ComponentModel.DataAnnotations;

namespace MNMovies_Backend.Models
{
    public class MovieModel
    {
        [Key]
        public int? MovieID { get; set; }
        public string MovieName { get; set; }
        public string MovieImage { get; set; }
        public string MovieVideo { get; set; }
        public string TypeOfMovie { get; set; }
        public string MovieCategory { get; set; }
        public string MovieType { get; set; }
        public string MovieLanguage { get; set; }
        public string MovieDescription { get; set; }
        public string MovieLength { get; set; }
        public DateTime MovieDate { get; set; }
        public int? MovieViews { get; set; }
    }
}
