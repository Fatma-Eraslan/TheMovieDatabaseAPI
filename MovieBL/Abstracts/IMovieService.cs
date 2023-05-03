using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBL.Abstracts
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        public Task AddMovieAsync(Movie movie);
        public Task<Movie> GetMovieById(int id);
    }
}
