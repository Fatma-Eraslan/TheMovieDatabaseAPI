using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBL.Abstracts
{
    public interface IUserService
    {
        Task<User> GetById(int id);
    }
}
