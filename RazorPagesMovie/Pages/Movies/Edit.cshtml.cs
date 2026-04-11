using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly IMovieRepo _movieRepo;
        private readonly IWebHostEnvironment _env;

        public EditModel(IMovieRepo movieRepo, IWebHostEnvironment env)
        {
            _movieRepo = movieRepo;
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

            var movie =  await _movieRepo.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = (Movie)movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // handle uploaded file (if any)
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                Movie.Image = PictureHelper.UploadNewImage(_env, files[0]);
            }
            else
            {
                // preserve existing image uri when no new file uploaded
                var existing = await _movieRepo.GetByIdAsync(Movie.Id);
                if (existing != null)
                {
                    Movie.Image = existing.Image;
                }
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _movieRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
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
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
