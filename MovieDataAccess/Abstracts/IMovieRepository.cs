using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Abstracts
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(Movie movie);
        public Task<Movie> GetMovieById(int id);
        //Task<List<Movie>> GetAllMoviesAsync();
        //Task<Movie> GetMovieByIdAsync(int id);        
        //Task UpdateMovieAsync(Movie movie);
        //Task DeleteMovieAsync(int id);
    }
}
