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
                var movie =await _context.FindAsync<Movie>(id);
                return movie;
            }
        }

        //public async Task<List<Movie>> GetAllMoviesAsync()
        //{
        //    return await _context.Movies.ToListAsync();
        //}

        //public async Task<Movie> GetMovieByIdAsync(int id)
        //{
        //    return await _context.Movies.FindAsync(id);
        //}              

        
    }
}
