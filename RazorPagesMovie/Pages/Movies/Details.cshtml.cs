using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieRepo _movieRepo;

        public Movie Movie { get; set; } = default!;

        public DetailsModel(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Movie = await _movieRepo.GetByIdAsync(id);
            return Page();
        }
    }
}
