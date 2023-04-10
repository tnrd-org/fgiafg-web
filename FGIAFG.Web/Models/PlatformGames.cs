namespace FGIAFG.Web.Models
{
    public class PlatformGames
    {
        public string Title { get; }
        public List<Game> Games { get; }

        public PlatformGames()
        {
            Title = "Unknown";
            Games = new List<Game>();
        }

        public PlatformGames(string title, List<Game> games)
        {
            Title = title;
            Games = games;
        }
    }
}