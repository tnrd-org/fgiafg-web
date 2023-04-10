namespace FGIAFG.Web.Models
{
    public class PlatformGamesCollection
    {
        public PlatformGamesCollection()
        {
            PlatformGames = new List<PlatformGames>();
        }

        public PlatformGamesCollection(List<PlatformGames> platformGames)
        {
            PlatformGames = platformGames;
        }

        public List<PlatformGames> PlatformGames { get; }

        public void Add(PlatformGames platformGames) => PlatformGames.Add(platformGames);
    }
}
