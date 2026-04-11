using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieRepo _movieRepo;

        public IEnumerable<Movie> Movie { get; set; } = default!;

        public IndexModel(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }


        public async void OnGetAsync()
        {
            Movie = await _movieRepo.GetAllAsync();
        }
    }
}
