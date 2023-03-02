using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonFetching.Models;
using System.Diagnostics.Metrics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PokemonFetching.Controllers
{
    public class LoginController : Controller
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public string api;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            api = _configuration.GetValue<string>("API");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto request)
        {
            Console.WriteLine(api);
            Registration receivedReservation = new Registration();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api + "/Auth/login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if(response.ReasonPhrase == "OK")
                    {
                        return Redirect("/Home");
                    }
                }
            }
            return Redirect("/Login");
        }
    }
}
