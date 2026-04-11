using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public interface IMovieRepo
    {
        Task AddAsync(Movie movie);//
        IEnumerable<Movie> GetAll();//
        Task<IEnumerable<Movie>> GetAllAsync();//
        Movie? GetById(int id);//
        Task<Movie?> GetByIdAsync(int? id);//
        Task RemoveAsync(Movie movie);//
        Task SaveChangesAsync();//
    }
}
