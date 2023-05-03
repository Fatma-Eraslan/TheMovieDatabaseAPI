using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEntities.Concretes
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
