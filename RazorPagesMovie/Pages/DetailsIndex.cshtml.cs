using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Pages.Movies
{
    public class DetailsIndexModel : PageModel
    {
        private readonly IMovieRepo _repo;
        public DetailsIndexModel(IMovieRepo repo)
        {
            _repo = repo;
        }
        public Movie Movie { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _repo.GetByIdAsync(id.Value);
            if (movie is not null)
            {
                Movie = movie;

                return Page();
            }
            return NotFound();
        }
    }
}
