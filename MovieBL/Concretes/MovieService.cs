using MovieBL.Abstracts;
using MovieDataAccess.Abstracts;
using MovieEntities.Concretes;
using MovieEntities.EntitiyDTO;
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


        public Task<Movie> GetMovieById(int id)
        {
            try
            {
                var result = _movie.GetMovieById(id);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var movies = await _movie.GetMovies();
                return movies;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Movie>> GetMovieByPages(int pageSize, int pageStart)
        {
            try
            {
                var movies = _movie.GetMovieByPages(pageSize, pageStart);
                return movies;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddMovieRating(int userId, int movieId, int vote, string note)
        {
            try
            {
                if (vote > 1 || vote < 10)
                {
                   await _movie.AddMovieRating(userId, movieId, vote, note);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<RatingsDto> GetMovieDetails(int movieId, int userId)
        {
            try
            {
                var result = _movie.GetMovieDetails(movieId, userId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
