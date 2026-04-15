using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IMovieRepo _movieRepo;

        public IList<Movie> Movie { get; set; } = default!;

        public IndexModel(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }

        

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
           var all = await _movieRepo.GetAllAsync();
            var genres = all.Select(m => m.Genre).Distinct().ToList();
            Genres = new SelectList(genres);
            Movie = all.Where(m => string.IsNullOrEmpty(SearchString) || m.Title.Contains(SearchString))
        .Where(m => string.IsNullOrEmpty(MovieGenre) || m.Genre == MovieGenre)
        .OrderBy(m => m.Rank)
        .ToList();
        }
    }
}
