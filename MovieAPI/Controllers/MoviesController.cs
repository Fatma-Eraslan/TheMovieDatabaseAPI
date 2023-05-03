using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBL.Abstracts;
using MovieDataAccess.Utilities.Result;
using MovieEntities.Concretes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MovieAPI.Controllers
{
    [Authorize]//!!
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movie;
        public MoviesController(IMovieService movie)
        {
            _movie = movie;
        }





        public async Task<Movie[]> GetMoviesFromApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5ZGNmMmJhNzdmOTc1YmY5ZTdlYWMyMjU1OTY5YjQxNyIsInN1YiI6IjY0NGJiZWZlZmJhNjI1MDU4MmQ1Y2UxOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RYceIA9Rtf7gcbvwr1qMxQaDf6MrKKcDL4C_k3iD4i0");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5ZGNmMmJhNzdmOTc1YmY5ZTdlYWMyMjU1OTY5YjQxNyIsInN1YiI6IjY0NGJiZWZlZmJhNjI1MDU4MmQ1Y2UxOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.RYceIA9Rtf7gcbvwr1qMxQaDf6MrKKcDL4C_k3iD4i0");
                var key = "9dcf2ba77f975bf9e7eac2255969b417";
                var apiUrl = $"https://api.themoviedb.org/3/movie/popular?api_key={key}&language=en-US";

                var response = await client.GetAsync(apiUrl);//API'den gelen yanıt

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(); //yanıtın içeriğini json formatında..
                    var result = JsonConvert.DeserializeObject<MovieResult>(content);//movie e dönüştür
                    return result.Results;
                    //var jsn = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    //var movies = await JsonSerializer.DeserializeAsync<MoviesResponse>(responseStream, jsn); //sil
                    //return Ok(movies);
                }
                else
                {
                    throw new Exception($"Error occurred while fetching data from API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }











    }
}
