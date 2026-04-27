using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Hosting;
using RazorPagesMovie.Helpers;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IMovieRepo _repo;
        private readonly IWebHostEnvironment _env;
        public EditModel(IMovieRepo repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }
        [BindProperty]
        public Movie Movie { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie =  await _repo.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }
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
            _repo.Update(Movie);
            try
            {
                await _repo.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.ExistsAsync(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
