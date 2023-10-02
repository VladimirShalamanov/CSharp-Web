namespace Watchlist.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Watchlist.Models;
    using Watchlist.Data.Models;
    using Watchlist.Services.Interface;

    [Authorize]
    public class MoviesController : Controller
    {
        protected string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await movieService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel()
            {
                Genres = await movieService.GetGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await movieService.AddMovieAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId)
        {
            string userId = GetUserId();

            await movieService.AddMovieToCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            await movieService
                .RemoveMovieFromCollectionAsync(movieId, GetUserId());

            return RedirectToAction(nameof(Watched));
        }

        [HttpGet]
        public async Task<IActionResult> Watched()
        {
            var model = await movieService.GetMyMoviesAsync(GetUserId());

            return View(model);
        }
    }
}
