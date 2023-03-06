using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonFetching.Models;
using System.Diagnostics;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace PokemonFetching.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public string api;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            api = _configuration.GetValue<string>("API");
        }
        public async Task<IActionResult> Index()
        {
            List<Country> reservationList = new List<Country>();
            using (var httpClient = new HttpClient())
            {
                // Access-Control-Allow-Origin
                httpClient.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                using (var response = await httpClient.GetAsync(api + "/Country"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Country>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        public ViewResult GetPokemon() => View();


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");

        }


        [HttpPost]
        public async Task<IActionResult> GetPokemon(int id)
        {
            Country reservation = new Country();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api + "/Country/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<Country>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(reservation);
        }


        public ViewResult AddPokemon() => View();

        [HttpPost]
        public async Task<IActionResult> AddPokemon(Country country)
        {
            Country receivedReservation = new Country();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api + "/Country", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> UpdateCountry(int id)
        {
            Country reservation = new Country();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api + "/Country/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Country>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry(Country country)
        {
            Country receivedReservation = new Country();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(api + "/Country/" + country.id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<Country>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> DeleteCountry(int CountryId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(api + "/Country/" + CountryId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        
    }
}