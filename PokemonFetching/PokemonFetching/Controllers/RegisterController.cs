using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonFetching.Models;
using System.Text;

namespace PokemonFetching.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        public string api;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
            api = _configuration.GetValue<string>("API");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto request)
        {
            Registration receivedReservation = new Registration();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api + "/Auth/register", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.ReasonPhrase == "OK")
                    {
                        return Redirect("/Login");
                    }
                }
            }
            return Redirect("/Register");
        }
    }
}
