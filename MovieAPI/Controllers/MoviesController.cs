using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBL.Abstracts;
using MovieDataAccess.Utilities.Result;
using MovieEntities.Concretes;
using MovieEntities.EntitiyDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Json;


namespace MovieAPI.Controllers
{
    [Authorize]//!!
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movie;
        private readonly IUserService _user;
        public MoviesController(IMovieService movie, IUserService user)
        {
            _movie = movie;
            _user = user;
        }

        [Description("Filmleri getir")]
        [Route("GetMovies")]
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


        [Description("Filmleri, başlangıç ve büyüklük parametresi alarak getiriyor")]//sayfa büyüklüğünü bu şekilde ele aldım
        [Route("GetMovies")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByPage(int pageSize, int pageStart)
        {

            var movies = await _movie.GetMovieByPages(pageSize, pageStart);
            if (movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }
        #region
        /* bu şekilde kullanıcıyı cookilerden alıyorum çalıştığım projede
        var jwt = Request.Cookies["jwt"];
        var token = await _tokenHelper.Verify(jwt);
        int userId = int.Parse(token.Issuer);
        var user = _user.GetByID(userId); 
        */
        #endregion


        [HttpPost("{userId}/{movieId}/vote/note")]
        [Description("Parametre olarak alınan verilerle filme kullanıcı tarafından not ve puan eklendi.")]
        public async Task<ActionResult> AddRating(int userId, int movieId, int vote, string note)
        {
            var user = await _user.GetById(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var movie = await _movie.GetMovieById(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            if (vote < 1 || vote > 10)
            {
                return BadRequest("Rating must be between 1 and 10");
            }
            var result = _movie.AddMovieRating(userId, movieId, vote, note);
            return Ok(result);
        }


        [HttpGet("{movieId}/{userId}")]
        [Description("Parametre olarak alınan kullanıcı ve film id sine göre film detaylarını etirme.")]
        public async Task<ActionResult> GetMovieDetails(int movieId, int userId)
        {
            var user = await _user.GetById(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var movie = await _movie.GetMovieById(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            var result = _movie.GetMovieDetails(movieId, userId);
            return Ok(result);
        }




        [HttpPost("{movieId}/{userId}/toemail")]
        [Description("Parametre olarak alınan mail adresine, simple mail transfer protokolünü kullanarak film tavsiye maili gönderme.")]
        public async Task<IActionResult> SendRecommendationEmailAsync(int movieId, int userId,string toemail)//mail servisi yazılabilir!
        {
            try
            {
                var user = await _user.GetById(userId);
                var movie = await _movie.GetMovieById(movieId);

                if (user == null || movie == null)
                {
                    return BadRequest();
                }

                var emailaddress = "sample@gmail.com";
                var password = "123";

                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(emailaddress, password),
                    EnableSsl = true
                };

                var message = new MailMessage(emailaddress, toemail)
                {
                    Subject = "Movie Recommendation",
                    Body = $"Hello, you have received a great movie recommendation from {user.Name}: {movie.Title}. Enjoy watching.",
                    IsBodyHtml = false
                };

                smtpClient.Send(message);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
