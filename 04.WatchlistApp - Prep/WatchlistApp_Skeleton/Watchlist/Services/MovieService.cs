namespace Watchlist.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using Watchlist.Data;
    using Watchlist.Models;
    using Watchlist.Data.Models;
    using Watchlist.Services.Interface;

    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext dbContext;

        public MovieService(WatchlistDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                GenreId = model.GenreId
            };

            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsync(int movieId, string userId)
        {
            bool alreadyAddedMovie = await dbContext.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (!alreadyAddedMovie)
            {
                var userMovie = new UserMovie()
                {
                    UserId = userId,
                    MovieId = movieId,
                };

                await dbContext.UserMovies.AddAsync(userMovie);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllMoviesViewModel>> GetAllAsync()
        {
            var entities = await dbContext
                .Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return entities
                .Select(m => new AllMoviesViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Genre = m.Genre.Name
                });

        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
                       => await dbContext.Genres.ToListAsync();

        public async Task<IEnumerable<AllMoviesViewModel>> GetMyMoviesAsync(string userId)
        {
            return await dbContext
                .UserMovies
                .Where(um => um.UserId == userId)
                .Select(m => new AllMoviesViewModel()
                {
                    Id = m.Movie.Id,
                    Title = m.Movie.Title,
                    Director = m.Movie.Director,
                    ImageUrl = m.Movie.ImageUrl,
                    Rating = m.Movie.Rating,
                    Genre = m.Movie.Genre.Name
                })
                .ToListAsync();
        }

        public async Task RemoveMovieFromCollectionAsync(int movieId, string userId)
        {
            var foundUserMovies = await dbContext.UserMovies
               .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (foundUserMovies != null)
            {
                dbContext.UserMovies.Remove(foundUserMovies);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
