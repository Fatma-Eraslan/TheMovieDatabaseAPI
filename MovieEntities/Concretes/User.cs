using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEntities.Concretes
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieRating> Ratings { get; set; }
    }
}
