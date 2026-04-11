using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMovieRepo _movieRepo;

        public IEnumerable<Movie> Movie { get; set; } = default!;

        public DetailsModel(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                 return NotFound();
            }
            var Movie = await _movieRepo.GetByIdAsync(id.Value);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
