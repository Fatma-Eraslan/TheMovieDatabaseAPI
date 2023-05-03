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
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetById(int id)
        {
            using (MovieDbContext _context = new MovieDbContext())
            {
                var user= await _context.Users.FindAsync(id);
                return user;
            }                
        }
    }
}
