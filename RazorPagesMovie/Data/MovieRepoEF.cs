using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data;

public class MovieRepoEF : IMovieRepo
{
    private readonly RazorPagesMovieContext _context;

    public MovieRepoEF(RazorPagesMovieContext context)
    {
        _context = context;
    }

    public IEnumerable<RazorPagesMovie.Models.Movie> GetAll()//
    {
        return _context.Movie.OrderBy(m => m.Rank).ThenBy(m => m.Title).ToList();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()//
    {
        return await _context.Movie.ToListAsync();  
    }


    public RazorPagesMovie.Models.Movie? GetById(int id)//
    {
        return _context.Movie.FirstOrDefault(m => m.Id == id);
    }


    public async Task<RazorPagesMovie.Models.Movie?> GetByIdAsync(int? id)//
    {
        return await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Movie movie)//
    {
        _context.Movie.Add(movie);
        await _context.SaveChangesAsync();
    }
   
    public async Task RemoveAsync(Movie movie)//
    {
        _context.Movie.Remove(movie);
        await _context.SaveChangesAsync();
    }
     public async Task SaveChangesAsync()//
    {
        await _context.SaveChangesAsync();
    }
}

