using MovieBL.Abstracts;
using MovieDataAccess.Abstracts;
using MovieEntities.Concretes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieBL.Concretes
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movie;

       
        public MovieService(IMovieRepository movie)
        {
            _movie = movie;
        }
        public Task AddMovieAsync(Movie movie)
        {
            try
            {
               var m= _movie.AddMovieAsync(movie);
                return m;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Movie> GetMovieById(int id)
        {
            try
            {
               var result= _movie.GetMovieById(id);
                return result;  
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Movie>> GetMovies()
        {
            throw new NotImplementedException();
        }

    }
}
