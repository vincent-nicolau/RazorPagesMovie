using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public interface IMovieRepo
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<IEnumerable<string>> GetGenresAsync();
        Task AddAsync(Movie movie);
        void Update(Movie movie);
        Task<bool> ExistsAsync(int id);
        void Delete(Movie movie);
        Task SaveAsync();
    }
}
