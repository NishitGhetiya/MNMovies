using System.ComponentModel.DataAnnotations;

namespace MNMovies.Models
{
    public class SeriesModel
    {
        [Key]
        public int? SeriesID { get; set; }
        public string SeriesName { get; set; }
        public string SeriesImage { get; set; }
        public string TypeOfSeries { get; set; }
        public string SeriesCategory { get; set; }
        public string SeriesType { get; set; }
        public string SeriesLanguage { get; set; }
        public string SeriesDescription { get; set; }
        public DateTime SeriesDate { get; set; }
        public int? SeriesView { get; set; }
    }
}
