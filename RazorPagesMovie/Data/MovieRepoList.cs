using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public class MovieRepoList : IMovieRepo
    {
        private readonly List<Movie> _movies = new List<Movie>
        {
            new Models.Movie { Id = 1, Title = "When Harry Met Sally", ReleaseDate = DateTime.Parse("1989-2-12"), Genre = "Romantic Comedy", Price = 7.99M, Rank = 4 },
            new Models.Movie { Id = 2, Title = "Ghostbusters ", ReleaseDate = DateTime.Parse("1984-3-13"), Genre = "Comedy", Price = 8.99M, Rank = 2 },
            new Models.Movie { Id = 3, Title = "Ghostbusters 2", ReleaseDate = DateTime.Parse("1986-2-23"), Genre = "Comedy", Price = 9.99M, Rank = 1 },
            new Models.Movie { Id = 4, Title = "Rio Bravo", ReleaseDate = DateTime.Parse("1959-4-15"), Genre = "Western", Price = 3.99M, Rank = 1 }
        };
        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            IEnumerable<Movie> movies = _movies
                .OrderBy(m => m.Rank)
                .ThenBy(m => m.Title)
                .ToList();

            return Task.FromResult(movies);
        }
        public Task<Movie?> GetByIdAsync(int id)
        {
            return Task.FromResult(_movies.FirstOrDefault(m => m.Id == id));
        }
        public Task<IEnumerable<string>> GetGenresAsync()
        {
            IEnumerable<string> genres = _movies
                .Select(m => m.Genre)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            return Task.FromResult(genres);
        }
        public Task AddAsync(Movie movie)
        {
            _movies.Add(movie);
            return Task.CompletedTask;
        }
        public void Update(Movie movie)
        {
            var existingMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);

            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.Genre = movie.Genre;
                existingMovie.Price = movie.Price;
                existingMovie.Rank = movie.Rank;
                existingMovie.PictureUri = movie.PictureUri;
            }
        }
        public Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(_movies.Any(m => m.Id == id));
        }
        public void Delete(Movie movie)
        {
            _movies.Remove(movie);
        }
        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}