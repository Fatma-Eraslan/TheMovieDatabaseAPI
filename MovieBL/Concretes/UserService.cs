using MovieBL.Abstracts;
using MovieDataAccess.Abstracts;
using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBL.Concretes
{
    public class UserService : IUserService //generiz bir repository de yazabilirdik!
    {
		private readonly IUserRepository _user;

        public UserService(IUserRepository user)
        {
            _user = user;
        }
        public async Task<User> GetById(int id)
        {
			try
			{
                var user=await _user.GetById(id);
                return user;
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
