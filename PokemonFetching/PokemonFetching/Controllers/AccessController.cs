using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PokemonFetching.Models;
using Newtonsoft.Json;
using System.Text;
using System.Windows;

namespace PokemonFetching.Controllers
{
    public class AccessController : Controller
    {
        private readonly IConfiguration _configuration;
        public string api;

        public AccessController(IConfiguration configuration)
        {
            _configuration = configuration;
            api = _configuration.GetValue<string>("API");
        }   
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    
                    var content = new StringContent(JsonConvert.SerializeObject(modelLogin), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(api + "/Registration/login", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        if (response.ReasonPhrase == "OK")
                        {
                            List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                        new Claim(ClaimTypes.Name, modelLogin.Email),
                        new Claim("OtherProperties","Example Role")
                    };

                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                                IsPersistent = modelLogin.KeepLoggedIn
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);

                            var cookieOptions = new CookieOptions();
                            cookieOptions.Expires = DateTime.Now.AddDays(1);
                            cookieOptions.Path = "/";
                            Response.Cookies.Append("Email", modelLogin.Email, cookieOptions);



                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            ViewData["ValidateMessage"] = "User Not Found";
            }
            return View();
        }




        public IActionResult Register()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration modelLogin)
        {
            if (ModelState.IsValid)
            {
                Registration receiveLogin = new Registration();
                using (var httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(modelLogin), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(api + "/Registration/registration", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.ReasonPhrase == "OK")
                        {
                            List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                        new Claim("OtherProperties","Example Role")
                    };

                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                                IsPersistent = modelLogin.KeepLoggedIn
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);

                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }
    }
}
