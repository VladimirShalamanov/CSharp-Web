namespace Watchlist.Services.Interface
{
    using Watchlist.Models;
    using Watchlist.Data.Models;

    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesViewModel>> GetAllAsync();
        Task<IEnumerable<Genre>> GetGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);
        Task AddMovieToCollectionAsync(int movieId, string userId);

        Task RemoveMovieFromCollectionAsync(int movieId, string userId);

        Task<IEnumerable<AllMoviesViewModel>> GetMyMoviesAsync(string userId);
    }
}
