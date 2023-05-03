using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEntities.Concretes
{
    public class MovieRating
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int Vote { get; set; } //puan
        public int UserId { get; set; }
        public int MovieId { get; set; }    
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
