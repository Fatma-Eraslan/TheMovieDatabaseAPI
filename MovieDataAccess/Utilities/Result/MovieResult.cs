using MovieEntities.Concretes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Utilities.Result
{
    public class MovieResult
    {
        [JsonProperty("results")]
        public Movie[] Results { get; set; } //API'den çekilen film sonuçlarını tut, çözümle
    }
}
