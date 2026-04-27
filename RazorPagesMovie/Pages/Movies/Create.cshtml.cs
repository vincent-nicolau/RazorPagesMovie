using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Hosting;
using RazorPagesMovie.Helpers;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly IMovieRepo _repo;
        private readonly IWebHostEnvironment _env;
        public CreateModel(IMovieRepo repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Movie Movie { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                Movie.PictureUri = PictureHelper.UploadNewImage(
                    _env,
                    HttpContext.Request.Form.Files[0]);
            }
            await _repo.AddAsync(Movie);
            await _repo.SaveAsync();
            return RedirectToPage("./Index");
        }
    }
}
