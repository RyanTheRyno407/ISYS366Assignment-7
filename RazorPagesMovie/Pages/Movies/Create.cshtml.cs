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
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly IMovieRepo _movieRepo;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IMovieRepo movieRepo,IWebHostEnvironment env)
        {
            _movieRepo = movieRepo;
            _env = env;
            
        }



        public IActionResult OnGet()
        {
            return Page();
        }

       

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(HttpContext.Request.Form.Files.Count > 0)
            {
                Movie.Image = PictureHelper.UploadNewImage(_env,
            HttpContext.Request.Form.Files[0]);
            }


           
            await _movieRepo.AddAsync(Movie);

            return RedirectToPage("./Index");
        }
    }
}
