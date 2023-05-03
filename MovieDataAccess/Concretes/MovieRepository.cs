using Microsoft.EntityFrameworkCore;
using MovieDataAccess.Abstracts;
using MovieDataAccess.Context;
using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Concretes
{
    public class MovieRepository : IMovieRepository
    {
        //private readonly MovieDbContext _context;

        //public MovieRepository(MovieDbContext context)
        //{
        //    _context = context;
        //}


        public async Task<Movie> GetMovieById(int id)
        {
            using (MovieDbContext _context = new MovieDbContext())
            {
                var movie = await _context.FindAsync<Movie>(id);
                return movie;
            }
        }

        public async Task<List<Movie>> GetMovies()
        {
            using (MovieDbContext _context = new MovieDbContext())
            {
                return await _context.Movies.ToListAsync();
            }
        }


        public async Task<List<Movie>> GetMovieByPages(int pageSize, int pageStart)
        {
            using (MovieDbContext _context = new MovieDbContext())
            {
                var movies = await _context.Movies.OrderBy(x => x.Id).Skip((pageStart - 1) * pageSize).Take(pageSize).ToListAsync();//başlangıç sayfa dahil et
                //
                return movies;
            }
        }

        public async Task AddMovieRating(int userId, int movieId, int vote, string note)
        {
            using (MovieDbContext _context = new MovieDbContext())
            {
                var existingRating = await _context.MoviesRatings.FirstOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);//zateb varsa ekletme, belki sora güncelleme modülü.
                if (existingRating == null)
                {
                    var movieRating = new MovieRating
                    {
                        MovieId = movieId,
                        UserId = userId,
                        Vote = vote,
                        Note = note
                    };
                    _context.MoviesRatings.Add(movieRating);
                    await _context.SaveChangesAsync();
                }              
                
            }
        }





    }
}
