namespace FGIAFG.Web.Models
{
    public class Game
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
