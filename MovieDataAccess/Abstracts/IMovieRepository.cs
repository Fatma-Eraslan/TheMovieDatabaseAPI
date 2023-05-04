using MovieEntities.Concretes;
using MovieEntities.EntitiyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Abstracts
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieById(int id);
        Task<List<Movie>> GetMovies();
        Task<List<Movie>> GetMovieByPages(int pageSize, int pageStart);
        Task AddMovieRating(int userId, int movieId, int vote, string note);
        Task<RatingsDto> GetMovieDetails(int movieId, int userId);
    }
}
