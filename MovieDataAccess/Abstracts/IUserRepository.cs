using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Abstracts
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
    }
}
