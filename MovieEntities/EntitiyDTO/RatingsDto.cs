using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEntities.EntitiyDTO
{
    public class RatingsDto
    {
        public string Title { get; set; }
        public string Overview { get; set; }
      
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public double Popularity { get; set; }
        public int Budget { get; set; }
        public bool Adult { get; set; }
        public string Note { get; set; }
        public int Vote { get; set; } //puan
        public double VoteAverage { get; set; }
    }
}
