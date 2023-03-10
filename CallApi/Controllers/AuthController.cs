using CallApi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CallApi.Controllers
{
    public class AuthController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var client   = _httpClientFactory.CreateClient("MyWebApp");
            var jsonBody = JsonConvert.SerializeObject(login);
            var content  = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/Token/GetToken" , content).Result;
            if (response.IsSuccessStatusCode)
            {
               var token= response.Content.ReadAsStringAsync().Result;
               var Claims = new List<Claim>()
                {
                  new Claim(ClaimTypes.NameIdentifier, login.UserName),
                  new Claim(ClaimTypes.Name, login.UserName),
                  new Claim("AccessToken" , token)
                };
                var identity = new ClaimsIdentity(Claims , CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh=true
                };
                HttpContext.SignInAsync(principal, properties);
                return Redirect("/Product/Index");
            }
            else
            {
                ModelState.AddModelError("UserName", "User Not valid");
                return View(login);
            }
           
        }
    }
}
