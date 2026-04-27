using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace RazorPagesMovie.Pages.Movies;
public class IndexModel : PageModel
{
    private readonly IMovieRepo _repo;
    public IndexModel(IMovieRepo repo)
    {
        _repo = repo;
    }
    public IList<Movie> Movie { get;set; } = default!;
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public SelectList? Genres { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? MovieGenre { get; set; }
    public async Task OnGetAsync()
    {
        var movies = await _repo.GetAllAsync();
        var genres = await _repo.GetGenresAsync();
        if (!string.IsNullOrEmpty(SearchString))
        {
            movies = movies.Where(s => s.Title.Contains(SearchString));
        }
        if (!string.IsNullOrEmpty(MovieGenre))
        {
            movies = movies.Where(x => x.Genre == MovieGenre);
        }
        Genres = new SelectList(genres);
        Movie = movies
            .OrderBy(m => m.Rank)
            .ThenBy(m => m.Title)
            .ToList();
    }
}
