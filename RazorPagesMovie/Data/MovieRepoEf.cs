using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public class MovieRepoEf : IMovieRepo
    {
        private readonly RazorPagesMovieContext _context;

        public MovieRepoEf(RazorPagesMovieContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movie
                .OrderBy(m => m.Rank)
                .ThenBy(m => m.Title)
                .ToListAsync();
        }
        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<IEnumerable<string>> GetGenresAsync()
        {
            return await _context.Movie
                .Select(m => m.Genre)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
        }
        public async Task AddAsync(Movie movie)
        {
            await _context.Movie.AddAsync(movie);
        }
        public void Update(Movie movie)
        {
            _context.Attach(movie).State = EntityState.Modified;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Movie.AnyAsync(e => e.Id == id);
        }
        public void Delete(Movie movie)
        {
            _context.Movie.Remove(movie);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
