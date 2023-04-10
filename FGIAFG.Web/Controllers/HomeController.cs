using FGIAFG.Web.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGIAFG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;

        public HomeController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            Result<PlatformGamesCollection> result = await GetGamesForPlatforms();

            if (result.IsSuccess)
            {
                result.Value.PlatformGames.Sort((x, y) => string.Compare(x.Title, y.Title, StringComparison.Ordinal));
                return View(result.Value);
            }
            else
            {
                return View(new PlatformGamesCollection());
            }
        }

        private async Task<Result<PlatformGamesCollection>> GetGamesForPlatforms()
        {
            Task<Result<PlatformGames>>[] tasks = new[]
            {
                GetGamesForPlatform("epic", "Epic Games"),
                GetGamesForPlatform("gog", "Good Old Games"),
                GetGamesForPlatform("playstation", "PlayStation"),
                GetGamesForPlatform("prime", "Prime Gaming"),
                // GetGamesForPlatform("steam"),
                GetGamesForPlatform("xbox", "Xbox Games With Gold"),
            };

            await Task.WhenAll(tasks);

            PlatformGamesCollection collection = new PlatformGamesCollection();

            foreach (Task<Result<PlatformGames>> task in tasks)
            {
                if (task.IsCompletedSuccessfully)
                {
                    Result<PlatformGames> result = task.Result;
                    if (result.IsSuccess && result.Value.Games.Count > 0)
                    {
                        collection.Add(result.Value);
                    }
                    else
                    {
                        // Add error to list somewhere, maybe log it?
                    }
                }
                else
                {
                    // Add error to list somewhere, maybe log it?
                }
            }

            return Result.Ok(collection);
        }

        private async Task<Result<PlatformGames>> GetGamesForPlatform(string key, string displayName)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"http://{key}.fgiafg.com");

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                return Result.Fail(new ExceptionalError(e));
            }

            string content = string.Empty;

            try
            {
                content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Result.Fail(new ExceptionalError(e));
            }

            if (string.IsNullOrEmpty(content))
                return Result.Fail(new Error("Content is null or empty"));

            List<Game>? games = null;

            try
            {
                games = JsonConvert.DeserializeObject<List<Game>>(content);
            }
            catch (Exception e)
            {
                return Result.Fail(new ExceptionalError(e));
            }

            if (games == null)
                return Result.Fail(new Error("Games is null"));

            return Result.Ok(new PlatformGames(displayName, games));
        }
    }
}
